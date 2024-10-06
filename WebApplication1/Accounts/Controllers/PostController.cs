using Accounts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Controllers
{
    public class PostController : Controller
    {
        private readonly Uri _baseAddress = new Uri("https://jsonplaceholder.typicode.com");

        // GET: PostsController/Index
        public async Task<ActionResult> Index()
        {
            List<Post> posts = new List<Post>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                HttpResponseMessage response = await client.GetAsync("/posts");

                if (response.IsSuccessStatusCode)
                {
                    string postsJson = await response.Content.ReadAsStringAsync();
                    posts = JsonConvert.DeserializeObject<List<Post>>(postsJson);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            return View(posts);
        }

        // GET: PostsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Post post = await GetPostById(id);
            return View(post);
        }

        // GET: PostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post newPost)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;

                    var jsonContent = JsonConvert.SerializeObject(newPost);
                    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("/posts", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Post post = await GetPostById(id);
            return View(post);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Post updatedPost)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;

                    var jsonContent = JsonConvert.SerializeObject(updatedPost);
                    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"/posts/{id}", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Post post = await GetPostById(id);
            return View(post);
        }

        // POST: PostsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = _baseAddress;

                    HttpResponseMessage response = await client.DeleteAsync($"/posts/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Helper method to get a post by ID
        private async Task<Post> GetPostById(int id)
        {
            Post post = new Post();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                HttpResponseMessage response = await client.GetAsync($"/posts/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string postJson = await response.Content.ReadAsStringAsync();
                    post = JsonConvert.DeserializeObject<Post>(postJson);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }

            return post;
        }
    }
}
