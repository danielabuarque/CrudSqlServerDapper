using CrudSqlServerDapper.Entities;
using CrudSqlServerDapper.Repositories;
using CrudSqlServerDapper.Validators;

namespace CrudSqlServerDapper.Controllers
{
    /// <summary>
    /// Represents a controller that manages client-related operations.
    /// </summary>
    public class ClientController
    {
        /// <summary>
        /// Method to execute the controller's operations.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("\nCLIENT SYSTEM REGISTRATION:\n");

            Console.WriteLine("(1) REGISTER A CLIENT");
            Console.WriteLine("(2) UPDATE A CLIENT");
            Console.WriteLine("(3) DELETE A CLIENT");
            Console.WriteLine("(4) SEARCH CLIENTS");

            Console.Write("\nENTER THE CHOSEN OPTION: ");
            string option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    CreateClient();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.Write("\nDO YOU WANT TO KEEP GOING? (Y,N): ");
            var continueOption = Console.ReadLine() ?? string.Empty;

            if(continueOption.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear(); //Clean the console for a new operation
                Execute();
            }
            else
            {
                Console.WriteLine("\nEND OF THE PROGRAM");
            }
        }

        /// <summary>
        /// Method to create a new client.
        /// </summary>
        private void CreateClient()
        {
            Console.WriteLine("\n REGISTRATION OF CLIENTS: \n");

            //Create a new instance of Client
            var client = new Client();

            Console.Write("ENTER THE NAME.............: ");
            client.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("ENTER THE EMAIL............: ");
            client.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("ENTER THE BIRTH DATE.......: ");
            client.BirthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

            //Instantiate the ClientValidator to validate the client data
            var validator = new ClientValidator();

            //Executing the validation on the client object
            var validationResult = validator.Validate(client);

            //Check if the validation was successful
            if(validationResult.IsValid)
            {
                //Create an instance of ClientRepository
                var clientRepository = new ClientRepository();
                clientRepository.Insert(client);

                Console.WriteLine("\nCLIENT REGISTERED SUCCESSFULLY!");
            }
            else
            {

                //If the validation fails, display the error messages
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine($"\tError: {error.ErrorMessage}");
                }
            }



        }
    }
}
