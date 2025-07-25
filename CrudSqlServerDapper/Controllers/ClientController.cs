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
            Console.WriteLine("(4) READ CLIENTS");

            Console.Write("\nENTER THE CHOSEN OPTION: ");
            string option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    CreateClient();
                    break;
                case "2":
                    UpdateClient();
                    break;
                case "3":
                    DeleteClient();
                    break;
                case "4":
                    ReadClients();
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
            if(!validationResult.IsValid)
            {
                Console.WriteLine("\nVALIDATION ERRORS OCURRED\n");
                //If the validation fails, display the error messages
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine($"\tError: {error.ErrorMessage}");
                }
                return; //Exit the method if validation fails
            }

            //If the validation is successful, proceed to insert the client into the database
            //Create an instance of ClientRepository
            var clientRepository = new ClientRepository();
            clientRepository.Insert(client);

            Console.WriteLine("\nCLIENT REGISTERED SUCCESSFULLY!");
        }

        /// <summary>
        /// Method to update an existing client.
        /// </summary>
        private void UpdateClient()
        {
            Console.WriteLine("\nUPDATING A CLIENT");
            Console.Write("\nENTER THE CLIENT'S ID YOU WANT TO UPDATE: ");
            var id = Guid.Parse(Console.ReadLine() ?? string.Empty);

            //Checking if the client exists in the database
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(id);  

            if(client != null)
            {
                Console.WriteLine("\nCLIENT DATA: ");
                Console.WriteLine($"\tID............: {client.Id}");
                Console.WriteLine($"\tNAME..........: {client.Name}");
                Console.WriteLine($"\tEMAIL.........: {client.Email}");
                Console.WriteLine($"\tBIRTHDATE.....: {client.BirthDate.ToShortDateString()}");

                Console.WriteLine("\nENTER THE DATA YOU WOULD LIKE TO UPDATE: \n");

                Console.Write("\nENTER THE NAME.............: ");
                client.Name = Console.ReadLine() ?? string.Empty;

                Console.Write("\nENTER THE EMAIL............: ");
                client.Email = Console.ReadLine() ?? string.Empty;

                Console.Write("\nENTER THE BIRTH DATE.......: ");
                client.BirthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

                //Instantiate the ClientValidator to validate the client data
                var validator = new ClientValidator();

                //Executing the validation on the client object
                var validationResult = validator.Validate(client);

                //Check if the validation was successful
                if (!validationResult.IsValid)
                {
                    Console.WriteLine("\nVALIDATION ERRORS OCURRED\n");
                    //If the validation fails, display the error messages
                    foreach (var error in validationResult.Errors)
                    {
                        Console.WriteLine($"\tError: {error.ErrorMessage}");
                    }
                    return; //Exit the method if validation fails
                }

                //If the validation is successful, proceed to update the client in the database
                clientRepository.Update(client);
                Console.WriteLine("\nCLIENT UPDATED SUCCESSFULLY!");
            }

            //If the client does not exist, display a message
            else
            {
                Console.WriteLine("\nCLIENT DOESN'T EXIST IN THE DATABASE!");
            }
        }

        /// <summary>
        /// Method to delete a client.
        /// </summary>
        private void DeleteClient()
        {
            Console.WriteLine("\nDELETING CLIENTS");
            Console.Write("\nENTER THE CLIENT'S ID YOU WANT TO DELETE: ");
            var id = Guid.Parse(Console.ReadLine() ?? string.Empty);

            //Checking if the client exists in the database
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(id);

            if (client != null)
            {
                Console.WriteLine("\nCLIENT DATA: ");
                Console.WriteLine($"\tID............: {client.Id}");
                Console.WriteLine($"\tNAME..........: {client.Name}");
                Console.WriteLine($"\tEMAIL.........: {client.Email}");
                Console.WriteLine($"\tBIRTHDATE.....: {client.BirthDate.ToShortDateString()}");

                Console.Write("\nDO YOU WANT TO DELETE THIS CLIENT? (Y,N)");
                var opcao = Console.ReadLine() ?? string.Empty;

                if(opcao.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    //If the client exists, proceed to delete it
                    clientRepository.Delete(id);
                    Console.WriteLine("\nCLIENT DELETED SUCCESSFULLY!");
                }
            }
            else
            {
                Console.WriteLine("\nCLIENT DOESN'T EXIST IN THE DATABASE!");
            }
        }

        /// <summary>
        /// Controller method to read and display clients.
        /// </summary>
        private void ReadClients()
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("     READING CLIENTS       ");
            Console.WriteLine("---------------------------\n");

            var clientRepository = new ClientRepository();  
            List<Client> clients = clientRepository.GetAll();

            foreach (var client in clients)
            {
                Console.WriteLine($"ID........: {client.Id}");
                Console.WriteLine($"Name......: {client.Name}");
                Console.WriteLine($"Email.....: {client.Email}");
                Console.WriteLine($"Birth Date: {client.BirthDate.ToShortDateString()}");
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
