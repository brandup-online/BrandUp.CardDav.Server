﻿using BrandUp.CardDav.Transport.Models.Abstract;

namespace BrandUp.CardDav.Transport.Models.Body
{
    public class Prop : IDavProperty
    {
        private string name;
        private string @namespace;
        private string key;

        string IXmlConvertMetadata.Name => name;
        string IXmlConvertMetadata.Namespace => @namespace;

        bool IEquatable<IDavProperty>.Equals(IDavProperty other)
        {
            return this.name == other.Name && this.@namespace == other.Namespace;
        }

        string IPropertyKey.Key => key;

        internal Prop(string name, string @namespace = "DAV:")
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.@namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
        }

        #region IDavProperty members

        IEnumerable<IXmlConvertMetadata> IXmlConvertMetadata.Inner => null;

        #endregion

        #region Static members

        public static Prop ETag => new("getetag");
        public static Prop CTag => new("getctag", "http://calendarserver.org/ns/");
        public static Prop CurrentUserPrincipal => new("current-user-principal");
        public static Prop PrincipalUrl => new("principal-URL");
        public static Prop CreationDate => new("creationdate");
        public static Prop DisplayName => new("displayname");
        public static Prop ContentLanguage => new("getcontentlanguage");
        public static Prop ContentLength => new("getcontentlength");
        public static Prop ContentType => new("getcontenttype");
        public static Prop LastModified => new("getlastmodified");
        public static Prop ResourceType => new("resourcetype");

        #endregion
    }
}
