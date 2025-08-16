// ============================================================================
// File: BlogController.cs
// Author: Utkarsh Tripathi
// Created On: 16-Aug-2025
// Description: Controller responsible for handling blog generation requests,
//              validating input, and returning results.
// ============================================================================

using AI_Blog_Generator.Models;
using AI_Blog_Generator.Repository;
using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;
using System.IO;
using System.Text;

namespace AI_Blog_Generator.Controllers
{
    /// <summary>
    /// Controller responsible for handling blog generation requests and returning results.
    /// </summary>
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;  // Repository for blog generation logic

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController"/> class.
        /// </summary>
        /// <param name="blogRepository">The repository used for blog generation.</param>
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        /// <summary>
        /// Returns the main blog input view with an empty <see cref="BlogRequest"/> model.
        /// </summary>
        /// <returns>The view for creating a blog request.</returns>
        public IActionResult Index()
        {
            return View(new BlogRequest());
        }

        /// <summary>
        /// Handles POST request to generate a blog.
        /// Validates the input, calls the repository, and returns a partial view with results.
        /// </summary>
        /// <param name="model">The blog request model containing user input.</param>
        /// <returns>
        /// A partial view with the generated blog content if successful,
        /// otherwise a <see cref="BadRequestObjectResult"/> containing validation errors.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> GenerateBlog(BlogRequest model)
        {
            try
            {
                // Validate model state
                if (!ModelState.IsValid)
                {
                    // Collect validation errors grouped by field
                    var errors = ModelState
                        .Where(ms => ms.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    // Return bad request with validation errors
                    return BadRequest(errors);
                }

                // Generate blog content using repository (AI logic inside repository)
                model = await _blogRepository.GenerateBlog(model);
            }
            catch (Exception ex)
            {
                // Log exception details (can be replaced with structured logging)
                Console.WriteLine(ex.ToString());
            }

            // Return partial view with the generated blog result
            return PartialView("_BlogResult", model);
        }
    }
}
