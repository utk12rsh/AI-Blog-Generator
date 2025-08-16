// ============================================================================
// File: BlogRepository.cs
// Author: Utkarsh Tripathi
// Created On: 16-Aug-2025
// Description: Repository implementation responsible for interacting with 
//              Google's Gemini AI to generate blog content based on user inputs.
// ============================================================================

using AI_Blog_Generator.Models;
using Markdig;
using Mscc.GenerativeAI;
using System.Text.RegularExpressions;

namespace AI_Blog_Generator.Repository
{
    /// <summary>
    /// Repository implementation for generating blogs using Google Gemini AI.
    /// </summary>
    public class BlogRepository : IBlogRepository
    {
        /// <summary>
        /// Generates a blog based on the given <see cref="BlogRequest"/> model.
        /// Calls the Gemini API with a constructed prompt and processes the response.
        /// </summary>
        /// <param name="blogRequest">The request containing topic, tone, language, and word count.</param>
        /// <returns>
        /// The same <see cref="BlogRequest"/> object with the <c>GeneratedBlog</c> 
        /// property populated with AI-generated HTML content.
        /// </returns>
        public async Task<BlogRequest> GenerateBlog(BlogRequest blogRequest)
        {
            try
            {
                // Retrieve the Google API key from environment variables
                string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");

                // Initialize the GoogleAI client with the API key
                var googleAI = new GoogleAI(apiKey);

                // Select the Gemini 1.5 Flash generative model
                var gemini = googleAI.GenerativeModel(Model.Gemini15Flash);

                // Construct the prompt using user inputs
                string prompt = $"Write a {blogRequest.WordCount}-word {blogRequest.Tone} blog post " +
                                $"about {blogRequest.Topic} in {blogRequest.Language} using simple words.";

                // Call the Gemini model to generate the blog content
                var response = await gemini.GenerateContent(prompt);

                // If the generated response contains text, convert Markdown to HTML
                if (!string.IsNullOrEmpty(response.Text))
                {
                    var pipeline = new MarkdownPipelineBuilder()
                                   .UseAdvancedExtensions() // Enables tables and other features
                                   .Build();

                    blogRequest.GeneratedBlog = Markdown.ToHtml(response.Text, pipeline);
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions encountered during API call or processing
                Console.WriteLine($"Error while generating blog: {ex.Message}");
            }

            // Return the updated BlogRequest with generated content (if any)
            return blogRequest;
        }
    }
}
