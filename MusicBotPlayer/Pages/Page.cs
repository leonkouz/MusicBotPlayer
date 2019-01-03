using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicBotPlayer
{
    public class Page : BaseViewModel
    {
        #region Private Properties

        /// <summary>
        /// The name of the page.
        /// </summary>
        private string name;

        /// <summary>
        /// The list of items associated with the page.
        /// </summary>
        private ObservableCollection<IPage> items = new ObservableCollection<IPage>();

        /// <summary>
        /// The child page of the page.
        /// </summary>
        private Page childPage;

        /// <summary>
        /// The parent page of the page.
        /// </summary>
        private Page parentPage;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// The name of the page.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                OnPropertyChanged("Name");
                name = value;
            }
        }

        /// <summary>
        /// The list of items associated with the page.
        /// </summary>
        public ObservableCollection<IPage> Items
        {
            get => items;
            set
            {
                OnPropertyChanged("Items");
                items = value;
            }
        }

        /// <summary>
        /// The child page of the page.
        /// </summary>
        public Page ChildPage
        {
            get => childPage;
            set
            {
                OnPropertyChanged("ChildPage");
                childPage = value;
                childPage.ParentPage = this;
            }
        }

        /// <summary>
        /// The parent page of the page.
        /// </summary>
        public Page ParentPage
        {
            get => parentPage;
            set
            {
                OnPropertyChanged("ParentPage");
                parentPage = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="name">The name of the page.</param>
        public Page(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items">The list of objects to populate <see cref="Items"/> with.</param>
        public Page(string name, ObservableCollection<IPage> items)
        {
            Name = name;
            Items = items;
        }

        public Page(string name, Page parentPage)
        {
            Name = name;
            Items = items;
            ParentPage = parentPage;
        }

        #endregion

    }
}