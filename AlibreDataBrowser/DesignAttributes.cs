using System;
using AlibreX;

namespace Bolsover.DataBrowser;

public class DesignAttribute : Attribute

{
    [Design]
    public enum AdExtendedDesignAttribute
    {
        [EnumMember] [TypeText("Comment")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_COMMENT)]
        AD_COMMENT = 0,

        [EnumMember] [TypeText("Cost Center")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_COST_CENTER)]
        AD_COST_CENTER = 1,

        [EnumMember] [TypeText("Created By")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_BY)]
        AD_CREATED_BY = 2,

        [EnumMember]
        [TypeText("Creating Application")]
        [ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATING_APPLICATION)]
        AD_CREATING_APPLICATION = 3,

        [EnumMember] [TypeText("Created Date")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_CREATED_DATE)]
        AD_CREATED_DATE = 4,

        [EnumMember] [TypeText("Document Number")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_DOCUMENT_NUMBER)]
        AD_DOCUMENT_NUMBER = 5,

        [EnumMember]
        [TypeText("Eng Approval Date")]
        [ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVAL_DATE)]
        AD_ENG_APPROVAL_DATE = 6,

        [EnumMember] [TypeText("Eng Approved By")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_ENG_APPROVED_BY)]
        AD_ENG_APPROVED_BY = 7,

        [EnumMember] [TypeText("Estimated Cost")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_ESTIMATED_COST)]
        AD_ESTIMATED_COST = 8,

        [EnumMember] [TypeText("Keywords")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_KEYWORDS)]
        AD_KEYWORDS = 9,

        [EnumMember] [TypeText("Last Author")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_AUTHOR)]
        AD_LAST_AUTHOR = 10,

        [EnumMember]
        [TypeText("Last Update Date")]
        [ExtendedDesignProperty(ADExtendedDesignProperty.AD_LAST_UPDATE_DATE)]
        AD_LAST_UPDATE_DATE = 11,

        [EnumMember] [TypeText("Material")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_MATERIAL)]
        AD_MATERIAL = 12,

        [EnumMember] [TypeText("Mfg Approved By")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_BY)]
        AD_MFG_APPROVED_BY = 13,

        [EnumMember]
        [TypeText("Mfg Approved Date")]
        [ExtendedDesignProperty(ADExtendedDesignProperty.AD_MFG_APPROVED_DATE)]
        AD_MFG_APPROVED_DATE = 14,

        [EnumMember] [TypeText("Modified")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_MODIFIED)]
        AD_MODIFIED = 15,

        [EnumMember] [TypeText("Product")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_PRODUCT)]
        AD_PRODUCT = 16,

        [EnumMember] [TypeText("Received From")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_RECEIVED_FROM)]
        AD_RECEIVED_FROM = 17,

        [EnumMember] [TypeText("Revision")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_REVISION)]
        AD_REVISION = 18,

        [EnumMember] [TypeText("Stock Size")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_STOCK_SIZE)]
        AD_STOCK_SIZE = 19,

        [EnumMember] [TypeText("Supplier")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_SUPPLIER)]
        AD_SUPPLIER = 20,

        [EnumMember] [TypeText("Title")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_TITLE)]
        AD_TITLE = 21,

        [EnumMember] [TypeText("Vendor")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_VENDOR)]
        AD_VENDOR = 22,

        [EnumMember] [TypeText("Web Link")] [ExtendedDesignProperty(ADExtendedDesignProperty.AD_WEBLINK)]
        AD_WEBLINK = 23
    }
}

public class TypeTextAttribute : Attribute
{
    public string TextType;

    public TypeTextAttribute(string textType)
    {
        TextType = textType;
    }
}

public class ExtendedDesignProperty : Attribute
{
    public ADExtendedDesignProperty Property;

    public ExtendedDesignProperty(ADExtendedDesignProperty property)
    {
        Property = property;
    }
}

public class EnumMemberAttribute : Attribute
{
}