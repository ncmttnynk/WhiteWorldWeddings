using System;
using System.Collections.Generic;
using System.Linq;
using CKEditor.NET;

/// <summary>
/// Summary description for Editor
/// </summary>
public class NYEditor
{
    public void StandartAyarlar(CKEditorControl editor)
    {
        editor.FilebrowserBrowseUrl = "/ckeditor/plugins/simogeo/Browser.aspx?type=files";
        editor.FilebrowserImageBrowseLinkUrl = "/ckeditor/plugins/simogeo/Browser.aspx?type=files";
        editor.FilebrowserImageBrowseUrl = "/ckeditor/plugins/simogeo/Browser.aspx?type=images";
        editor.FilebrowserFlashBrowseUrl = "";

        Ayarla(editor);
    }
    public void StandartAyarlar(CKEditorControl editor, string klasor)
    {
        editor.FilebrowserBrowseUrl = "/ckeditor/plugins/simogeo/Browser.aspx?type=files&Klasor=" + klasor;
        editor.FilebrowserImageBrowseLinkUrl = "/ckeditor/plugins/simogeo/Browser.aspx?type=files&Klasor=" + klasor;
        editor.FilebrowserImageBrowseUrl = "/ckeditor/plugins/simogeo/Browser.aspx?type=images&Klasor=" + klasor;
        editor.FilebrowserFlashBrowseUrl = "/ckeditor/plugins/simogeo/Browser.aspx?type=flash&Klasor=" + klasor;

        Ayarla(editor);
    }
    void Ayarla(CKEditorControl editor)
    {
        editor.config.toolbar = AdminEditorToolbar;
        editor.config.enterMode = EnterMode.P;
        editor.config.shiftEnterMode = EnterMode.BR;
    }
    public void BasitAyarlar(CKEditorControl editor)
    {
        //editor.FilebrowserBrowseUrl = "/ckeditor/plugins/simogeo/Browser.aspx";
        //editor.FilebrowserImageBrowseUrl = "/ckeditor/plugins/simogeo/Browser.aspx";
        editor.config.toolbar = BasitToolbar;
        editor.config.enterMode = EnterMode.P;
        editor.config.shiftEnterMode = EnterMode.BR;
    }
    public object[] FullToolbar
    {
        get
        {
            return new object[]
			{
				new object[] { "Source", "-", "Save", "Preview", "-"},
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt" },
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
				new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				new object[] { "BidiLtr", "BidiRtl" },
				new object[] { "Link", "Unlink", "Anchor" },
				new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe" },
				"/",
				new object[] { "Styles", "Format", "Font", "FontSize" },
				new object[] { "TextColor", "BGColor" },
				new object[] { "Maximize", "ShowBlocks", "-", "About" }
			};
        }
    }
    public object[] AdminEditorToolbar
    {
        get
        {
            return new object[]
			{
				new object[] { "Source", "-", "Maximize", "ShowBlocks","NewPage", "Preview"},
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord"},
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote" },
				new object[] { "TextColor", "BGColor" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				new object[] { "Link", "Unlink", "Anchor", "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", 
								"Format", "Font", "FontSize" }
			};
        }
    }
    public object[] BasitToolbar
    {
        get
        {
            return new object[]
			{
				new object[] { "Preview","-", "Maximize","-" },
				new object[] { "Undo", "Redo" },
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-"},
				new object[] { "Link", "Unlink", "Image", "Table", "HorizontalRule" },
				"/",
				new object[] { "RemoveFormat","-", "TextColor", "BGColor" ,"-","Bold", "Italic", "Underline", "Strike" },
				new object[] { "NumberedList", "BulletedList" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" }				
			};
        }
    }
}
