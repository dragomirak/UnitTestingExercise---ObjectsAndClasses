using NUnit.Framework;

using System;
using System.Reflection.Metadata;
using TestApp.Store;
using TestApp.Vehicle;

namespace TestApp.UnitTests;

public class ArticleTests
{
    private Article _article;

    [SetUp]
    public void SetUp()
    {
        this._article = new();
    }


    [Test]
    public void Test_AddArticles_ReturnsArticleWithCorrectData()
    {
        // Arrange
        string[] articles = new string[]
        {
            "Article Content Author",
            "Article2 Content2 Author2",
            "Article3 Content3 Author3"
        };

        // Act
        Article result = this._article.AddArticles(articles);

        // Assert
        Assert.That(result.ArticleList, Has.Count.EqualTo(3));
        Assert.That(result.ArticleList[0].Title, Is.EqualTo("Article"));
        Assert.That(result.ArticleList[1].Content, Is.EqualTo("Content2"));
        Assert.That(result.ArticleList[2].Author, Is.EqualTo("Author3"));
    }

    [Test]
    public void Test_GetArticleList_SortsArticlesByTitle()
    {
        // Arrange
        Article inputArticle = new Article()
        {
            ArticleList = new()
            {
                new Article
                {
                    Title = "Salad",
                    Content = "Recipe",
                    Author = "Ivan"
                },
                new Article
                {
                    Title = "Soup",
                    Content = "Ingredients",
                    Author = "Maria"
                }
            }
        };
        string expected = $"Salad - Recipe: Ivan{Environment.NewLine}Soup - Ingredients: Maria";

        // Act
        string result = this._article.GetArticleList(inputArticle, "title");

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Test_GetArticleList_ReturnsEmptyString_WhenInvalidPrintCriteria()
    {
        // Arrange
        Article inputArticle = new Article()
        {
            ArticleList = new()
            {
                new Article
                {
                    Title = "Salad",
                    Content = "Recipe",
                    Author = "Ivan"
                },
                new Article
                {
                    Title = "Soup",
                    Content = "Ingredients",
                    Author = "Maria"
                }
            }
        };

        // Act
        string result = this._article.GetArticleList(inputArticle, "followers");

        // Assert
        Assert.That(result, Is.Empty);
    }
}
