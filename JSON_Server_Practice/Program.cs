using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace JSON_Server_Practice
{
    internal class Program
    {
        private static  RestClient _client = new RestClient("http://localhost:3000");


        static async Task Main()
        {
            //File.Create(@"C:\Users\vaish\JSON-Data-Files\EmpDb.json");



            //create get request
            //var request = new RestRequest("Employees", Method.Get);

            ////Execute the request
            //var response = _client.Execute(request);

            ////check response status
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    Console.WriteLine("Data retrieved successfully");

            //deserialize the response content
            //List<Employee> empList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            ////print the employee details
            //if (empList != null)
            //{
            //    foreach (var employee in empList)
            //    {
            //        Console.WriteLine($"Id: {employee.EmployeeId}, Name: {employee.EmployeeName}, Salary: {employee.Salary}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"Error: {response.StatusCode}, Message: {response.Content}");
            //}
            //Console.WriteLine("Press any key to exit..");
            //Console.ReadKey();

            //initialize the restClient


            //call different methods to interact with api
            //Console.WriteLine("Getting employee list..");
            //await GetEmployeeList();

            //Console.WriteLine("Adding new employee");
            //await AddNewEmployee("1", "Dharma", "45000");

            //Console.WriteLine("Adding multiple employees");
            //await AddMultipleEmployees();


            //Console.WriteLine("Updating employee salary...");
            //await UpdateEmployeeSalary("1", "85000");

            //Console.WriteLine("Deleting an employee...");
            await DeleteEmployee("3");

            //Console.WriteLine("All operations completed.");


            // }
        }

        private static async Task GetEmployeeList()
        {
            var request = new RestRequest("Employees", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<Employee> empList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
                foreach (var employee in empList)
                {
                    Console.WriteLine($"Id: {employee.EmployeeId}, Name: {employee.EmployeeName}, Salary: {employee.Salary}");

                }
            }
            else
            {
                Console.WriteLine($"Failed to get employee list. Status: {response.StatusCode}");

            }
        }

        private static async Task AddNewEmployee(string id, string name, string salary)
        {
            var request = new RestRequest("Employees", Method.Post);
            var jsonObj = new 
            {
                id = id,
                name = name,
                salary = salary
            };
            request.AddJsonBody(jsonObj);

            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var employee = JsonConvert.DeserializeObject<Employee>(response.Content);

                Console.WriteLine($"added employee: {employee.EmployeeName}, Salary:{employee.Salary}");

            }
            else
            {
                Console.WriteLine($"Failed to add employee. Status :{response.StatusCode}");

            }
        }

        private static async Task AddMultipleEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = "1", EmployeeName = "Tom", Salary = "24000" },
                new Employee { EmployeeId = "2", EmployeeName = "Jerry", Salary = "45000" },
                new Employee { EmployeeId = "3", EmployeeName = "Shak", Salary = "30000" }

            };
            foreach(var employee in employees)
            {
                var request = new RestRequest("Employees", Method.Post);
                var jsonObj = new
                {
                    id = employee.EmployeeId,
                    name = employee.EmployeeName,
                    salary = employee.Salary
                };
                request.AddJsonBody(jsonObj);
                var response= await _client.ExecuteAsync(request);

                if(response.StatusCode == HttpStatusCode.Created)
                {
                    var addedEmp = JsonConvert.DeserializeObject<Employee>(response.Content);
                    Console.WriteLine($"added employee: {addedEmp.EmployeeName}");
                }
                else
                {
                    Console.WriteLine($"Failed to add employee {employee.EmployeeName}. Status: {response.StatusCode}");

                }
            }

        }


        private static async Task UpdateEmployeeSalary(string id,string newSalary)
        {
            var request = new RestRequest($"Employees/{id}", Method.Put);
            var jsonObj = new
            {
                salary = newSalary,
            };
            request.AddJsonBody(jsonObj);
            var response=await _client.ExecuteAsync(request);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                var updatedEmp = JsonConvert.DeserializeObject<Employee>(response.Content);
                Console.WriteLine($"Updated Employee: {updatedEmp.EmployeeName}, new salary: {updatedEmp.Salary}");
            }
            else
            {
                Console.WriteLine($"Failed to update employee salary. Status:{response.StatusCode}");

            }
        }


        private static async Task DeleteEmployee(string id)
        {
            var request = new RestRequest($"Employees/{id}", Method.Delete);
            var response= await _client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"employee successfully deleted with id: {id}");
            }
            else
            {
                Console.WriteLine($"Failed to delete employee with id:{id}. Status: {response.StatusCode}");
            }
        }



    }
}
