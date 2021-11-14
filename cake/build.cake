var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./src/Example/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    foreach (var projFIle in GetProjFiles())
    {
        DotNetCoreBuild(projFIle.FullPath, new DotNetCoreBuildSettings
        {
            Configuration = configuration,
        });        
    }    
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
});



//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);


//////////////////////////////////////////////////////////////////////
// PRIVATES
//////////////////////////////////////////////////////////////////////

private FilePathCollection GetProjFiles(bool log = true)
{
    var files = GetFiles("../src/Papers/**/*.csproj");
    if (log)
    {
        foreach (var file in files)
        {
            Information(file.FullPath);
        }
    }

    return files;
}