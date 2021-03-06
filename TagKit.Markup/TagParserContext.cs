﻿using System;
using System.Text;
using TagKit.Markup.Properties;

namespace TagKit.Markup
{
    public class TagParserContext
    {
        private TagNameTable _nt = null;
        private TagNamespaceManager _nsMgr = null;
        private String _docTypeName = String.Empty;
        private String _pubId = String.Empty;
        private String _sysId = String.Empty;
        private String _internalSubset = String.Empty;
        private String _xmlLang = String.Empty;
        private TagSpace _xmlSpace;
        private String _baseURI = String.Empty;
        private Encoding _encoding = null;

        public TagParserContext(TagNameTable nt, TagNamespaceManager nsMgr, String xmlLang, TagSpace xmlSpace)
        : this(nt, nsMgr, null, null, null, null, String.Empty, xmlLang, xmlSpace)
        {
            // Intentionally Empty
        }

        public TagParserContext(TagNameTable nt, TagNamespaceManager nsMgr, String xmlLang, TagSpace xmlSpace, Encoding enc)
        : this(nt, nsMgr, null, null, null, null, String.Empty, xmlLang, xmlSpace, enc)
        {
            // Intentionally Empty
        }

        public TagParserContext(TagNameTable nt, TagNamespaceManager nsMgr, String docTypeName,
                  String pubId, String sysId, String internalSubset, String baseURI,
                  String xmlLang, TagSpace xmlSpace)
        : this(nt, nsMgr, docTypeName, pubId, sysId, internalSubset, baseURI, xmlLang, xmlSpace, null)
        {
            // Intentionally Empty
        }

        public TagParserContext(TagNameTable nt, TagNamespaceManager nsMgr, String docTypeName,
                          String pubId, String sysId, String internalSubset, String baseURI,
                          String xmlLang, TagSpace xmlSpace, Encoding enc)
        {
            if (nsMgr != null)
            {
                if (nt == null)
                {
                    _nt = nsMgr.NameTable;
                }
                else
                {
                    if ((object)nt != (object)nsMgr.NameTable)
                    {
                        throw new TagException(Resources.Xml_NotSameNametable, string.Empty);
                    }
                    _nt = nt;
                }
            }
            else
            {
                _nt = nt;
            }

            _nsMgr = nsMgr;
            _docTypeName = (null == docTypeName ? String.Empty : docTypeName);
            _pubId = (null == pubId ? String.Empty : pubId);
            _sysId = (null == sysId ? String.Empty : sysId);
            _internalSubset = (null == internalSubset ? String.Empty : internalSubset);
            _baseURI = (null == baseURI ? String.Empty : baseURI);
            _xmlLang = (null == xmlLang ? String.Empty : xmlLang);
            _xmlSpace = xmlSpace;
            _encoding = enc;
        }

        public TagNameTable NameTable
        {
            get
            {
                return _nt;
            }
            set
            {
                _nt = value;
            }
        }

        public TagNamespaceManager NamespaceManager
        {
            get
            {
                return _nsMgr;
            }
            set
            {
                _nsMgr = value;
            }
        }

        public String DocTypeName
        {
            get
            {
                return _docTypeName;
            }
            set
            {
                _docTypeName = (null == value ? String.Empty : value);
            }
        }

        public String PublicId
        {
            get
            {
                return _pubId;
            }
            set
            {
                _pubId = (null == value ? String.Empty : value);
            }
        }

        public String SystemId
        {
            get
            {
                return _sysId;
            }
            set
            {
                _sysId = (null == value ? String.Empty : value);
            }
        }

        public String BaseURI
        {
            get
            {
                return _baseURI;
            }
            set
            {
                _baseURI = (null == value ? String.Empty : value);
            }
        }

        public String InternalSubset
        {
            get
            {
                return _internalSubset;
            }
            set
            {
                _internalSubset = (null == value ? String.Empty : value);
            }
        }

        public String XmlLang
        {
            get
            {
                return _xmlLang;
            }
            set
            {
                _xmlLang = (null == value ? String.Empty : value);
            }
        }

        public TagSpace XmlSpace
        {
            get
            {
                return _xmlSpace;
            }
            set
            {
                _xmlSpace = value;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return _encoding;
            }
            set
            {
                _encoding = value;
            }
        }

        internal bool HasDtdInfo
        {
            get
            {
                return (_internalSubset != string.Empty || _pubId != string.Empty || _sysId != string.Empty);
            }
        }
    }  
}
