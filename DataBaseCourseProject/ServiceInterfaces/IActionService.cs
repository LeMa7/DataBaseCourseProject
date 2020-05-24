namespace DataBaseCourseProject.ServiceInterfaces
{
    public interface IActionService
    {
        string GetDiff(int? firstId, int? secondId);
    }
}