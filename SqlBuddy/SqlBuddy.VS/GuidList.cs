namespace SqlBuddy.VisualStudio
{
    internal static class GuidList
    {
        public const string SqlBuddyGeneratorGuidString = "EB6B87B8-C542-4505-9683-B955F3FB5745";
        public const string SqlBuddyVisualStudioPkgString = "47DD7ED2-CDD8-4972-8D6C-B1D170E09464";

        // Visual Studio Custom Tools are language-specific (that is, they are available, or not available, 
        // depending on the programming language associated with the project that wants to use the custom tool). 
        // Therefore, you must specify a CodeGeneratorRegistration attribute for each language you want to support; 
        // the third argument is a GUID that identifies a project type in Visual Studio. The GUIDs for C#, VB and J# are
        public const string VsContextGuidVCSProject = "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}";
        public const string VsContextGuidVBProject = "{164B10B9-B200-11D0-8C61-00A0C91E29D5}";
        public const string VsContextGuidVJSProject = "{E6FDF8B0-F3D1-11D4-8576-0002A516ECE8}"; 
    }
}