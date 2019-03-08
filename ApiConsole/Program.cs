using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ApiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cluedintestapi.herokuapp.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // To get all companies
                HttpResponseMessage response = await client.GetAsync("api/companies");

                if (response.IsSuccessStatusCode)
                {
                    List<Company> company = await response.Content.ReadAsAsync<List<Company>>();
                    for (int i = 0; i < company.Count; i++)
                    {
                        List<Company> companies = new List<Company>();

                        companies.Add(new Company()
                        {
                            id = company[i].id,
                            name = company[i].name,
                            email = company[i].email,
                            phoneNumber = company[i].phoneNumber
                        });

                        string output = JsonConvert.SerializeObject(companies);
                        Console.WriteLine(output);
                    }
                }
                Console.WriteLine("\n");
                // To get a certain company
                response = await client.GetAsync("api/companies/1");

                if (response.IsSuccessStatusCode)
                {
                    Company company = await response.Content.ReadAsAsync<Company>();
                    string output = JsonConvert.SerializeObject(company);
                    Console.WriteLine(output);
                }

                Console.WriteLine("\n");

                // To get all employees data from a company
                response = await client.GetAsync("api/companies/1/employees");

                if (response.IsSuccessStatusCode)
                {
                    List<Employee> employee = await response.Content.ReadAsAsync<List<Employee>>();
                    for (int i = 0; i < employee.Count; i++)
                    {
                        List<Employee> employees = new List<Employee>();

                        employees.Add(new Employee()
                        {
                            id = employee[i].id,
                            firstName = employee[i].firstName,
                            lastName = employee[i].lastName,
                            companyId = employee[i].companyId,
                            title = employee[i].title,
                            email = employee[i].email
                        });

                        string output = JsonConvert.SerializeObject(employees);
                        Console.WriteLine(output);
                    }
                }

                Console.WriteLine("\n");

                // To get get all customers data from a company
                response = await client.GetAsync("api/companies/1/customers");

                if (response.IsSuccessStatusCode)
                {
                    List<Customer> customer = await response.Content.ReadAsAsync<List<Customer>>();
                    for (int i = 0; i < customer.Count; i++)
                    {
                        List<Customer> customers = new List<Customer>();

                        customers.Add(new Customer()
                        {
                            id = customer[i].id,
                            customerName = customer[i].customerName,
                            companyId = customer[i].companyId,
                            customerValue = customer[i].customerValue
                        });

                        string output = JsonConvert.SerializeObject(customers);
                        Console.WriteLine(output);
                    }

                    Console.ReadKey();
                }

            }
        }
    }
}