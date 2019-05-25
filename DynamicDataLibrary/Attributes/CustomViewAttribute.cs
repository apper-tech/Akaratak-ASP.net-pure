using NotAClue.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =false)]
   public class CustomViewAttribute:Attribute
    {
        string[] customList;
        bool hideFilters;
        string customRes;
        bool floor;
        int length;
        /// <summary>
        /// Hide the filters view in certan templates
        /// </summary>
        public bool HideFilters
        {
            get
            {
                return hideFilters;
            }

            set
            {
                hideFilters = value;
            }
        }
        PageTemplate hideColumnInTemplate;
        /// <summary>
        /// ORginal Override of Hide
        /// </summary>
        public PageTemplate HideColumnInTemplate
        {
            get
            {
                return hideColumnInTemplate;
            }

            set
            {
                hideColumnInTemplate = value;
            }
        }

        string coulmn_Names_File_Name;
        /// <summary>
        /// Used in Display pages and filters
        /// </summary>
        public bool HideColumnIfEmpty
        {
            get
            {
                return hideColumnIfEmpty;
            }

            set
            {
                hideColumnIfEmpty = value;
            }
        }
        /// <summary>
        /// A Multi-Purpus String
        /// </summary>
        public string CustomText
        {
            get
            {
                return customText;
            }

            set
            {
                customText = value;
            }
        }
        /// <summary>
        /// A Multi-Purpus String list
        /// </summary>
        public string[] CustomList
        {
            get
            {
                return customList;
            }

            set
            {
                customList = value;
            }
        }
        /// <summary>
        /// For colum Dynamic Description File Name
        /// </summary>
        public string Coulmn_Names_File_Name
        {
            get
            {
                return coulmn_Names_File_Name;
            }

            set
            {
                coulmn_Names_File_Name = value;
            }
        }
        /// <summary>
        /// Call Custom Resource file name 
        /// </summary>
        public string CustomRes
        {
            get
            {
                return customRes;
            }

            set
            {
                customRes = value;
            }
        }
        /// <summary>
        /// used for specific floor numbering 
        /// </summary>
        public bool Floor
        {
            get
            {
                return floor;
            }

            set
            {
                floor = value;
            }
        }
        /// <summary>
        /// Replace the Empty String with default Value
        /// </summary>
        public bool EmptyString
        {
            get
            {
                return emptyString;
            }

            set
            {
                emptyString = value;
            }
        }

        public int Length { get => length; set => length = value; }

        bool hideColumnIfEmpty;
        string customText;
        bool emptyString;
    }
}
