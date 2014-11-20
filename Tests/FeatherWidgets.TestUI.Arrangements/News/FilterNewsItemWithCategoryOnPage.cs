﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SearchForNotExistingTag arrangement class.
    /// </summary>
    public class FilterNewsItemWithCategoryOnPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            List<string> categories = new List<string>();

            ServerOperations.Taxonomies().CreateCategory(TaxonTitle + "0");
            int index = 0;
            for (int i = 1; i < 7; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(TaxonTitle + i, TaxonTitle + (i - 1));
                categories.Add(TaxonTitle + i);               
            }
            foreach (var category in categories)
            {                
                var cat = new List<string> { category };
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + index, NewsContent, "AuthorName", "SourceName", cat, null, null);
                index++;
            }

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
        }

        private const string PageName = "News";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string TaxonTitle = "Category";
    }
}
