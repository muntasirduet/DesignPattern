using DesignPattern.SOLID;

namespace DesignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            //SRP (Single Responsibility Principle)
            //SingleResponsibilityPrinciple singleResponsibility = new SingleResponsibilityPrinciple();
            //singleResponsibility.Run();

            //OCP (Open/Closed Principle)
            //OpenClosedPrinciple openClosed = new OpenClosedPrinciple();
            //openClosed.Run();

            //LSP (Liskov Substitution Principle)
            //LiskovSubstitutionPrinciple liskovSubstitution = new LiskovSubstitutionPrinciple();
            //liskovSubstitution.Run();

            //ISP (Interface Segregation Principle) and Dependency Inversion Principle
            InterfaceSegregationPrinciple interfaceSegregation = new InterfaceSegregationPrinciple();
            interfaceSegregation.Run();
        }
    }
}
