using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    public class PageController : BaseViewModel
    {
        #region Private Properties

        /// <summary>
        /// The collection of pages.
        /// </summary>
        private ObservableCollection<Page> pages = new ObservableCollection<Page>();

        /// <summary>
        /// The page currently selected.
        /// </summary>
        private Page currentPage;

        #endregion

        #region Public Properties

        /// <summary>
        /// The collection of pages.
        /// </summary>
        private ObservableCollection<Page> Pages
        {
            get => pages;
            set
            {
                OnPropertyChanged("Pages");
                pages = value;
            }
        }

        /// <summary>
        /// The page currently selected.
        /// </summary>
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                OnPropertyChanged("CurrentPage");
                currentPage = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PageController()
        {
        }

        #endregion

        /// <summary>
        /// Creates and adds a page to the list and populates the page with the specified list of objects.
        /// </summary>
        /// <param name="pageName">The name of the page.</param>
        /// <param name="items">The items associated with the page.</param>
        public void AddPage(string pageName, ICollection<object> items)
        {
            Page newPage = new Page(pageName);
            Pages.Add(newPage);
        }

        /// <summary>
        /// Adds the specified page to the list.
        /// </summary>
        /// <param name="page"></param>
        public void AddPage(Page page)
        {
            Pages.Add(page);
        }

        /// <summary>
        /// Creates and adds a page to the list.
        /// </summary>
        /// <param name="pageName">The name of the page.</param>
        public void AddPage(string pageName)
        {
            Page newPage = new Page(pageName);
            Pages.Add(newPage);
        }

        /// <summary>
        /// Retrieves the page with the specified name.
        /// </summary>
        /// <param name="pageName">The name of the page to retrieve.</param>
        /// <returns>The list of pages that contain the specified name.</returns>
        public IEnumerable<Page> GetPages(string pageName)
        {
            return Pages.Where(x => x.Name == pageName);
        }

        /// <summary>
        /// Retrives the first occurence of the specified page name
        /// in the list.
        /// </summary>
        /// <param name="pageName">The name of the page to retrieve.</param>
        /// <returns>The first occurence of the specified page name.</returns>
        public Page GetPage(string pageName)
        {
            var pageSearch = Pages.Where(x => x.Name == pageName);

            if (pageSearch.Count() != 0)
            {
                return pageSearch.First();
            }
            else
            {
                return SearchAllPages(pageName);
            }
        }

        /// <summary>
        /// Navigate to child page. Does nothing if a
        /// child page does not exist.
        /// </summary>
        public void NavigateToChildPage()
        {
            if (CurrentPage.ChildPage != null)
            {
                CurrentPage = CurrentPage.ChildPage;
            }
        }

        /// <summary>
        /// Navigate to parent page. Does nothing if a
        /// parent page does not exist.
        /// </summary>
        public void NavigateToParentPage()
        {
            if (CurrentPage.ParentPage != null)
            {
                CurrentPage = CurrentPage.ChildPage;
            }
        }

        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="page"></param>
        public void NavigateToPage(Page page)
        {
            CurrentPage = page;
        }

        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="pageName">The name of the page.</param>
        public void NavigateToPage(string pageName)
        {
            CurrentPage = GetPage(pageName);
        }

        private Page SearchAllPages(string pageName)
        {
            foreach (Page pg in Pages)
            {
                if (pg.Name == pageName)
                {
                    return pg;
                }
                else
                {
                    var page = SearchChildPages(pg, pageName);
                    if (page != null)
                    {
                        return page;
                    }
                }
            }

            return null;
        }

        public Page SearchChildPages(Page pg, string pageName)
        {
            bool finished = false;
            Page page = pg;
            Page childPage = pg.ChildPage;

            while (finished == false)
            {
                if (childPage != null)
                {
                    if (childPage.Name == pageName)
                    {
                        return childPage;
                    }
                    else
                    {
                        page = childPage;
                        childPage = page.ChildPage;
                    }
                }
                else
                {
                    finished = true;
                }
            }

            return null;
        }

        /// <summary>
        /// Clear the items of all pages.
        /// </summary>
        public void ClearItemsOfAllPages()
        {
            foreach (Page pg in Pages)
            {
                if (pg.Items != null)
                {
                    pg.Items.Clear();
                }

                ClearChildPages(pg);
            }
        }

        /// <summary>
        /// Iterates through the child page of the page and clears all items
        /// until all child pages are cleared.
        /// </summary>
        /// <param name="pg">The page to iterate through.</param>
        private void ClearChildPages(Page pg)
        {
            bool finished = false;
            Page page = pg;
            Page childPage = pg.ChildPage;

            while (finished == false)
            {
                if (childPage != null && childPage.Items != null && childPage.Items.Count != 0)
                {
                    childPage.Items.Clear();
                    page = childPage;
                    childPage = page.ChildPage;
                }
                else
                {
                    finished = true;
                }
            }
        }
    }
}