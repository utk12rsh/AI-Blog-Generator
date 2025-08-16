# 🚀 AI Blog Generator

**Author:** Utkarsh Tripathi  
**Created On:** 16-Aug-2025  

---

![AI Blog Generator](https://img.shields.io/badge/AI-Blog%20Generator-blue?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-6+-green?style=for-the-badge)
![Google Gemini](https://img.shields.io/badge/Google-Gemini15Flash-yellow?style=for-the-badge)

---

## 📌 Description

AI Blog Generator is a **web application** that generates **high-quality blog posts** on any topic using **Google Gemini AI API**. The generated content is converted from **Markdown → HTML** and styled with **CSS**, while interactivity is powered by **jQuery**.  

> 💡 This project uses the **Repository Design Pattern** to keep the code clean and maintainable.

---

## ✨ Features

- 📝 Generate blog posts on **any topic**.  
- 🎨 Customize **Tone** (Casual, Formal, Humorous), **Language** (English/Hindi), and **Word Count**.  
- ⚡ Dynamic prompt construction sent to **Gemini15Flash** AI model.  
- 🔄 Converts AI-generated **Markdown content to HTML** using **Markdig**.  
- 🎨 Fully styled with **CSS** for a clean interface.  
- 🖱 Interactivity via **jQuery**, including **copy-to-clipboard** for code blocks.  
- ✅ Form validations implemented using **Data Annotations**.

---

## 🤖 About Gemini15Flash

**Gemini15Flash** is a generative AI model from Google’s Gemini series.  
It produces **high-quality, human-like content** by understanding instructions, following prompts, and generating coherent text.

> In this project, Gemini15Flash generates **Markdown content** dynamically from user inputs like topic, tone, language, and word count.

---

## ⚙ How It Works

<details>
<summary>Click to expand</summary>

1. User fills out the form with:  
   - **Topic**  
   - **Tone** (Casual, Formal, Humorous)  
   - **Language** (English/Hindi)  
   - **Word Count**
2. App **constructs a dynamic prompt** using these inputs.  
3. Sends the prompt to **Gemini15Flash AI**.  
4. AI generates **Markdown content**.  
5. Markdown is converted to **HTML using Markdig**.  
6. HTML is styled with **CSS** and enhanced with **jQuery** for interactivity.

</details>

---

## 🗂 Project Structure

- **Controllers**: Handle HTTP requests (`BlogController.cs`).  
- **Models**: Define data structure & validation (`BlogRequest.cs`).  
- **Repository**: Business logic & Gemini AI integration (`BlogRepository.cs`, `IBlogRepository.cs`).  
- **Views**: Razor views for form & blog display.  
- **wwwroot/css**: CSS styling files.  
- **wwwroot/js**: JavaScript files (jQuery) for interactivity.

---

## ⚡ Prerequisites

- **Visual Studio 2022 or later**  
- **.NET 6 or later**  
- **NuGet Packages**:
  1. `Mscc.GenerativeAI`
  2. `Markdig`  

- **Google API Key** (for Gemini AI access)  
[Generate Google API KEY](https://console.cloud.google.com/apis/credentials)

> 📝 **Important:** Use the key name `GOOGLE_API_KEY` as referenced in the code (`BlogRepository.cs`).  

---

## 🛠 Set Google API Key

After generating your Google API Key, set it as an environment variable by running below command in powershell:

```powershell
setx GOOGLE_API_KEY your_api_key_here
