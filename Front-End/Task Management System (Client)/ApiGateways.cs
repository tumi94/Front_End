using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Task_Management_System__Client_.Models;
using Task = Task_Management_System__Client_.Models.Task;

namespace Task_Management_System__Client_
{
    public class ApiGateways
    {
        private string url = "https://localhost:7215/api/tasks";
        private HttpClient httpClient = new HttpClient();

        public List<Task> GetAllTasks()
        {
            List<Task> tasks = new List<Task>();

            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            try
            {
                HttpResponseMessage responseMessage = httpClient.GetAsync(url).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string result = responseMessage.Content.ReadAsStringAsync().Result;
                    tasks = JsonConvert.DeserializeObject<List<Task>>(result);
                }
                else
                {
                    string result = responseMessage.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occurred at the API Endpoint. Error Info: " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred at the API Endpoint. Error Info: " + ex.Message);
            }

            return tasks;
        }

        public Task AddTask(Task task)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            // Convert the enum to string
            task.Status = task.Status.ToString();

            string json = JsonConvert.SerializeObject(task);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Task>(result);
                    if (data != null)
                    {
                        task = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint, Error Info.  " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the Endpoint, Error Info.  " + ex.Message);
            }
            finally
            {
                // Cleanup or additional operations
            }
            return task;
        }


        public Task GetTaskById(int id)
        {
            Task task = new Task();
            url = $"{url}/{id}";

            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Task>(result);
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occurred at the API Endpoint. Error Info: " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred at the API Endpoint. Error Info: " + ex.Message);
            }
        }

        public void UpdateTask(Task task)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            int id = task.Id;
            url = $"{url}/{id}";

            string json = JsonConvert.SerializeObject(task);

            try
            {
                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occurred at the API Endpoint. Error Info: " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred at the API Endpoint. Error Info: " + ex.Message);
            }
        }

        public void DeleteTask(int id)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            url = $"{url}/{id}";

            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occurred at the API Endpoint. Error Info: " + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred at the API Endpoint. Error Info: " + ex.Message);
            }
        }
    }
}
