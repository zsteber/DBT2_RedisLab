namespace Redis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Methods ops = new Methods();

            ops.CreateEmployee();

            ops.ReadEmployee();
            
            ops.UpdateEmployee();
            
            ops.DeleteEmployee();
        }
    }
}