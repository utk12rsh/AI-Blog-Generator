// ============================================================================
// File: IBlogRepository.cs
// Author: Utkarsh Tripathi
// Created On: 16-Aug-2025
// Description: Interface defining the contract for a blog repository that 
//              handles AI-based blog generation.
// ============================================================================

using AI_Blog_Generator.Models;

namespace AI_Blog_Generator.Repository
{
    /// <summary>
    /// Defines the contract for generating blogs using AI.
    /// </summary>
    public interface IBlogRepository
    {
        /// <summary>
        /// Generates a blog based on the provided <see cref="BlogRequest"/> model.
        /// </summary>
        /// <param name="blogRequest">The request object containing topic, tone, language, and word count.</param>
        /// <returns>
        /// A task representing the asynchronous operation, with the updated 
        /// <see cref="BlogRequest"/> containing the generated blog content.
        /// </returns>
        Task<BlogRequest> GenerateBlog(BlogRequest blogRequest);
    }
}
