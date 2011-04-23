// Jon Dick http://www.codeproject.com/KB/cs/ExtensionManagerLibrary.aspx

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ExtensionManager
{
    /// <summary>
    /// A Generics enabled Framework for discovering and hosting Compiled and Source File Extensions
    /// </summary>
    public class ExtensionManager<ClientInterface, HostInterface>
    {
        #region Constructor

        public ExtensionManager()
        {
        }

        #endregion Constructor

        #region Events, Delegates, Event Helpers

        public delegate void AssemblyLoadingEventHandler(object sender, AssemblyLoadingEventArgs e);
        public event AssemblyLoadingEventHandler AssemblyLoading;

        private void OnAssemblyLoading(AssemblyLoadingEventArgs e)
        {
            if (this.AssemblyLoading != null)
                this.AssemblyLoading(this, e);
        }

        public delegate void AssemblyLoadedEventHandler(object sender, AssemblyLoadedEventArgs e);
        public event AssemblyLoadedEventHandler AssemblyLoaded;

        private void OnAssemblyLoaded(AssemblyLoadedEventArgs e)
        {
            if (this.AssemblyLoaded != null)
                this.AssemblyLoaded(this, e);
        }

        public delegate void AssemblyFailedLoadingEventHandler(object sender, AssemblyFailedLoadingEventArgs e);
        public event AssemblyFailedLoadingEventHandler AssemblyFailedLoading;

        private void OnAssemblyFailedLoading(AssemblyFailedLoadingEventArgs e)
        {
            if (this.AssemblyFailedLoading != null)
                this.AssemblyFailedLoading(this, e);
        }

        #endregion Events, Delegates, Event Helpers

        #region Private Variables

        private Dictionary<string, SourceFileLanguage> sourceFileExtensionMappings = new Dictionary<string, SourceFileLanguage>();
        private List<Extension<ClientInterface>> extensions = new List<Extension<ClientInterface>>();
        private List<string> compiledFileExtensions = new List<string>();
        private List<string> sourceFileReferencedAssemblies = new List<string>();

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// Contains file extensions (string) mapped to what Language they should be compiled in (SourceFileLanguage).
        /// To populate this List with the defaults, call LoadDefaultFileExtensions()
        /// </summary>
        public Dictionary<string, SourceFileLanguage> SourceFileExtensionMappings
        {
            get { return sourceFileExtensionMappings; }
            set { sourceFileExtensionMappings = value; }
        }

        /// <summary>
        /// List of Instances of all Loaded Extensions (both compiled and source file)
        /// </summary>
        public List<Extension<ClientInterface>> Extensions
        {
            get { return extensions; }
            set { extensions = value; }
        }

        /// <summary>
        /// List of file extensions to try loading as Compiled Assemblies.
        /// To populate this List with the defaults, call LoadDefaultFileExtensions()
        /// </summary>
        public List<string> CompiledFileExtensions
        {
            get { return compiledFileExtensions; }
            set { compiledFileExtensions = value; }
        }

        /// <summary>
        /// List of Namespaces that Source Files may reference when being compiled.
        /// By Default this list is empty.
        /// </summary>
        public List<string> SourceFileReferencedAssemblies
        {
            get { return sourceFileReferencedAssemblies; }
            set { sourceFileReferencedAssemblies = value; }
        }

        #endregion Properties

        #region Public Methods

        public void UnloadExtension(Extension<ClientInterface> extension)
        {
            Extension<ClientInterface> toRemove = null;

            foreach (Extension<ClientInterface> extOn in this.Extensions)
            {
                if (extOn.Filename.ToLower().Trim() == extension.Filename.ToLower().Trim())
                {
                    toRemove = extOn;
                    break;
                }
            }

            Extensions.Remove(toRemove);
        }

        /// <summary>
        /// Loads the SourceFileExtensionMappings and CompiledFileExtensions Lists with default values.
        /// Default Values Include:
        /// SourceFileExtensionMappings - .cs = CSharp, .vb = Vb, .js = Javascript
        /// CompiledFileExtensions - .dll
        /// </summary>
        public void LoadDefaultFileExtensions()
        {
            sourceFileExtensionMappings.Add(".cs", SourceFileLanguage.CSharp);
            sourceFileExtensionMappings.Add(".vb", SourceFileLanguage.Vb);
            sourceFileExtensionMappings.Add(".js", SourceFileLanguage.Javascript);
            compiledFileExtensions.Add(".dll");
        }

        /// <summary>
        /// Searches a given folder for files matching the SourceFileExtensionMappings and CompiledFileExtensions and
        /// attempts to load instances of those files.  Non-Recursive.
        /// </summary>
        /// <param name="folderPath">Full path to load files from</param>
        public void LoadExtensions(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (string fileOn in Directory.GetFiles(folderPath))
                {
                    LoadExtension(fileOn);
                }
            }
        }

        /// <summary>
        /// Attempts to load an instance of the given file if it matches SourceFileExtensionMappings or CompiledFileExtensions
        /// </summary>
        /// <param name="filename">Full name of file to load</param>
        public void LoadExtension(string filename)
        {
            // Fire off the loading event, gives consumer a chance to cancel the loading
            AssemblyLoadingEventArgs eargs = new AssemblyLoadingEventArgs(filename);
            OnAssemblyLoading(eargs);

            // If the event consumer cancelled it, no need to continue for this file
            if (eargs.Cancel)
                return;

            // Get the extension of the file
            string extension = new FileInfo(filename).Extension.TrimStart('.').Trim().ToLower();

            // Check to see if the extension is in the list of source file extensions
            // This allows us to pair up an extension with a particular language it should be compiled in
            // Primative but otherwise how do we know what to compile it as?  We could do some deep analysis of file content,
            // but that is beyond the scope of this library in my opinion
            if (SourceFileExtensionMappings.ContainsKey(extension) || SourceFileExtensionMappings.ContainsKey("." + extension))
            {
                SourceFileLanguage language = SourceFileLanguage.CSharp;

                // Get the matching language
                if (SourceFileExtensionMappings.ContainsKey(extension))
                    language = SourceFileExtensionMappings[extension];
                else
                    language = SourceFileExtensionMappings["." + extension];

                // Obviously it's a source file, so load it
                this.LoadSourceFile(filename, language);
            }
            else if (CompiledFileExtensions.Contains(extension) || CompiledFileExtensions.Contains("." + extension))
            {
                // It's in the compiled file extension list, so just load it
                this.LoadCompiledFile(filename);
            }
            else
            {
                // Unknown extension, raise the failed loading event
                AssemblyFailedLoadingEventArgs e = new AssemblyFailedLoadingEventArgs(filename);
                e.ExtensionType = ExtensionType.Unknown;
                e.ErrorMessage = "File (" + filename + ") does not match any SourceFileExtensionMappings or CompiledFileExtensions and cannot be loaded.";
                this.OnAssemblyFailedLoading(e);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadSourceFile(string filename, SourceFileLanguage language)
        {
            bool loaded = false;
            string errorMsg = "";

            // Try compiling the script first
            CompilerResults res = CompileScript(filename, sourceFileReferencedAssemblies, this.GetCodeDomLanguage(language));

            // Check for compilation errors
            if (res.Errors.Count <= 0)
            {
                // No errors, then loop through the types in the assembly
                // We don't stop after the first time we find our interface, this way the
                // Assembly or source file could have multiple types with the desired interface
                // Instead of a 1 interface per file relationship
                foreach (Type t in res.CompiledAssembly.GetTypes())
                {
                    // Get the string name of the ClientInterface interface
                    string typeName = typeof(ClientInterface).ToString();

                    // Try getting the clientinterface from the type
                    if (t.GetInterface(typeName, true) != null)
                    {
                        try
                        {
                            // Load an instance of this particular Extension and add it to our extensions list
                            Extension<ClientInterface> newExt = new Extension<ClientInterface>(filename, ExtensionType.SourceFile, (ClientInterface)res.CompiledAssembly.CreateInstance(t.FullName, true));
                            newExt.InstanceAssembly = res.CompiledAssembly;

                            extensions.Add(newExt);
                            OnAssemblyLoaded(new AssemblyLoadedEventArgs(filename));
                            loaded = true;
                        }
                        catch (Exception ex)
                        {
                            // Some problem in actually creating an instance
                            errorMsg = "Error Creating Instance of Compiled Source File (" + filename + "): " + ex.Message;
                        }
                    }
                }

                // We got through without loading an instance, so we didn't find types with the expected interface
                if (!loaded && String.IsNullOrEmpty(errorMsg))
                    errorMsg = "Expected interface (" + typeof(ClientInterface).ToString() + ") was not found in any types in the compiled Source File";
            }
            else
            {
                // Compile time errors
                errorMsg = "Source File Compilation Errors were Detected";
            }

            if (!loaded)
            {
                // Instance was never created, so let's report it and why
                AssemblyFailedLoadingEventArgs e = new AssemblyFailedLoadingEventArgs(filename);
                e.ExtensionType = ExtensionType.SourceFile;
                e.SourceFileCompilerErrors = res.Errors;
                e.ErrorMessage = errorMsg;
                this.OnAssemblyFailedLoading(e);
            }
        }

        private void LoadCompiledFile(string filename)
        {
            bool loaded = false;
            string errorMsg = "";
            Assembly compiledAssembly = null;

            // Load the assembly to memory so we don't lock up the file
            byte[] assemblyFileData = System.IO.File.ReadAllBytes(filename);

            // Load the assembly
            try
            {
                compiledAssembly = Assembly.Load(assemblyFileData);
            }
            catch
            {
                errorMsg = "Compiled Assembly (" + filename + ") is not a valid Assembly File to be Loaded.";
            }

            if (compiledAssembly != null)
            {
                // Go through the types we need to find our clientinterface
                foreach (Type t in compiledAssembly.GetTypes())
                {
                    // Just the string name of our ClientInterface since it's unknown at compile time
                    string typeName = typeof(ClientInterface).ToString();

                    // Try getting the interface from the current type
                    if (t.GetInterface(typeName, true) != null)
                    {
                        try
                        {
                            // Load an instance of this particular Extension and add it to our extensions list
                            Extension<ClientInterface> newExt = new Extension<ClientInterface>(filename, ExtensionType.Compiled,
                                (ClientInterface)compiledAssembly.CreateInstance(t.FullName, true));
                            newExt.InstanceAssembly = compiledAssembly;

                            extensions.Add(newExt);
                            OnAssemblyLoaded(new AssemblyLoadedEventArgs(filename));
                            loaded = true;
                        }
                        catch (Exception ex)
                        {
                            // Creating an instance failed for some reason, pass along that exception message
                            errorMsg = "Error Creating Instance of Compiled Assembly (" + filename + "): " + ex.Message;
                        }
                    }
                }

                // If no instances were loaded at this point, means we never found types with the ClientInterface
                if (!loaded && String.IsNullOrEmpty(errorMsg))
                {
                    errorMsg = "Expected interface (" + typeof(ClientInterface).ToString() + ") was not found in Compiled Assembly (" + filename + ")";
                }
            }

            if (!loaded)
            {
                // Nothing was loaded, report it
                AssemblyFailedLoadingEventArgs e = new AssemblyFailedLoadingEventArgs(filename);
                e.ExtensionType = ExtensionType.Compiled;
                e.ErrorMessage = errorMsg;
                this.OnAssemblyFailedLoading(e);
            }
        }

        private CompilerResults CompileScript(string filename, List<string> references, string language)
        {
            CodeDomProvider cdp = CodeDomProvider.CreateProvider(language);

            // Configure parameters
            CompilerParameters parms = new CompilerParameters();
            parms.GenerateExecutable = false; // Don't make exe file
            parms.GenerateInMemory = true; // Don't make ANY file, do it in memory
            parms.IncludeDebugInformation = false; // Don't include debug symbols

            // Add references passed in
            if (references != null)
            {
                parms.ReferencedAssemblies.AddRange(references.ToArray());
            }

            // Compile
            CompilerResults results = cdp.CompileAssemblyFromFile(parms, filename);

            return results;
        }

        private string GetCodeDomLanguage(SourceFileLanguage language)
        {
            string result = "C#";

            switch (language)
            {
                case SourceFileLanguage.CSharp:
                    result = "C#";
                    break;
                case SourceFileLanguage.Vb:
                    result = "VB";
                    break;
                case SourceFileLanguage.Javascript:
                    result = "JS";
                    break;
            }

            return result;
        }

        #endregion Private Methods
    }
}