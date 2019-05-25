using System;

namespace NotAClue.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Upload attribute defines values for the upload
    /// field templates
    /// </summary>
    /// <remarks></remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UploadAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Gets or sets the height to display the image, 
        /// if only one of the two dimensions are specified
        /// then the aspect ration will be retained.
        /// </summary>
        /// <value>The height.</value>
        /// <remarks></remarks>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width to display the image, 
        /// if only one of the two dimensions are specified
        /// then the aspect ration will be retained.
        /// </summary>
        /// <value>The width.</value>
        /// <remarks></remarks>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the uploads folder.
        /// </summary>
        /// <value>The uploads folder.</value>
        /// <remarks></remarks>
        public String UploadFolder { get; set; }

        /// <summary>
        /// Gets or sets the icons folder.
        /// </summary>
        /// <value>The icons folder.</value>
        /// <remarks></remarks>
        public String ImagesFolder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show hyperlink].
        /// </summary>
        /// <value><c>true</c> if [show hyperlink]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public Boolean ShowHyperlink { get; set; }

        /// <summary>
        /// Gets or sets the file types.
        /// </summary>
        /// <value>The file types.</value>
        /// <remarks></remarks>
        public String[] FileTypes { get; set; }

        /// <summary>
        /// Gets or sets the image extension.
        /// </summary>
        /// <value>The image extension.</value>
        /// <remarks></remarks>
        public String ImageExtension { get; set; }
        #endregion

        /// <summary>
        /// Initialize a upload atribute
        /// </summary>
        /// <param name="imageext"> the extention of the image to upload</param>
        /// <param name="imageFolder"> where to put the images</param>
        /// <param name="filet"> the file types allowed</param>
        /// <param name="widthh"> the widthfor aspect ratio</param>
        /// <param name="uploadfolder"> the temp folder for upload</param>
        /// <param name="showh"> show the hyperlink</param>
        /// <param name="hig"> the hight</param>
        public UploadAttribute(string imageext,string imageFolder,string[] filet,int widthh,string uploadfolder,bool showh,int hig)
        {
            // set a default value of 50 and constrain aspect ratio.
            Height = hig;
            // set default images folder
            UploadFolder = uploadfolder;
            //set image ext
            ImageExtension = imageext;
            //set the image folder
            ImagesFolder = imageFolder;
            //show the hyperlink
            ShowHyperlink = showh;
            //file types
            FileTypes = filet;
            //width
            Width = widthh;
        }
        public UploadAttribute() { }
    }
}