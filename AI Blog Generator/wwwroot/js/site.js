/* ============================================================================
   File: site.js
   Author: Utkarsh Tripathi
   Created On: 16-Aug-2025
   Description: Handles AJAX submission of blog form, displays loader and results,
                highlights code comments, and adds copy-to-clipboard buttons.
============================================================================ */

$(document).ready(function () {

    // Initial call: add copy buttons and highlight comments if code blocks already exist
    addCopyButtonsAndHighlightComments(document);

    // Hide loader initially
    $('#loader').hide();

    // Handle blog form submission via AJAX
    $('#blogForm').on('submit', function (e) {
        e.preventDefault();

        $('#blog-result').hide();
        $('#loader').show();
        $('#topicError, #toneError, #wordCountError, #languageError').text('');

        var formData = {
            Topic: $('#topic').val(),
            Tone: $('#tone').val(),
            WordCount: $('#wordCount').val(),
            Language: $('#language').val()
        };

        $.ajax({
            url: '/Blog/GenerateBlog',
            type: 'POST',
            data: formData,
            success: function (response) {
                // Clear form inputs
                $('#topic, #tone, #wordCount, #language').val("");

                // Display the generated blog
                $('#blog-result').show();
                $('#blog-result').html(response);

                // Add copy buttons and highlight comments in the new content
                addCopyButtonsAndHighlightComments('#blog-result');
            },
            error: function (xhr) {
                $('#loader').hide();
                if (xhr.status === 400) {
                    var errors = xhr.responseJSON;
                    if (errors.Topic) $('#topicError').text(errors.Topic.join(', '));
                    if (errors.Tone) $('#toneError').text(errors.Tone.join(', '));
                    if (errors.WordCount) $('#wordCountError').text(errors.WordCount.join(', '));
                    if (errors.Language) $('#languageError').text(errors.Language.join(', '));
                } else {
                    alert('An unexpected error occurred: ' + xhr.statusText);
                }
            },
            complete: function () {
                $('#blog-result').show();
                $('#loader').hide();
            }
        });
    });

    // Copy to clipboard button handler
    $(document).on('click', '.copy-btn', function () {
        const $btn = $(this);
        const codeText = $btn.siblings('pre').find('code').text();

        navigator.clipboard.writeText(codeText).then(() => {
            $btn.text('Copied!').css('color', '#de1f52');
            setTimeout(() => $btn.text('Copy').css('color', ''), 2000);
        }).catch(err => {
            console.error('Failed to copy text: ', err);
        });
    });
});

/**
 * Adds copy buttons and highlights code comments in all <pre><code> blocks within the container
 * @param {HTMLElement|string} container - DOM element or selector containing code blocks
 */
function addCopyButtonsAndHighlightComments(container) {
    $(container).find('pre code').each(function () {
        let html = $(this).html();

        // Highlight single-line comments (//)
        html = html.replace(/(\/\/.*)/g, '<span style="color:#de1f52;">$1</span>');

        // Highlight multi-line comments (/* ... */)
        html = html.replace(/(\/\*[\s\S]*?\*\/)/g, '<span style="color:#de1f52;">$1</span>');

        // Highlight single-line comments (#) for Python/Bash
        html = html.replace(/(^|\s)(#.*)/g, '$1<span style="color:#de1f52;">$2</span>');

        $(this).html(html);

        // Wrap <pre> in a container for copy button positioning
        const $wrapper = $('<div class="code-container" style="position:relative;"></div>');

        // Avoid wrapping multiple times
        if (!$(this).parent().parent().hasClass('code-container')) {
            $(this).parent().wrap($wrapper);
        }

        // Append copy button if not already added
        if ($(this).parent().parent().find('.copy-btn').length === 0) {
            const $copyBtn = $('<button class="copy-btn" style="position:absolute; top:5px; right:5px;">Copy</button>');
            $(this).parent().parent().append($copyBtn);
        }
    });
}
