$(function () {

    var LunarSFX = {};

    LunarSFX.GridManager = {

        // function to create grid to manage posts
        postsGrid: function (gridName, pagerName) {

            var afterclickPgButtons = function (whichbutton, formid, rowid) {
                tinymce.get("ShortDescription").setContent(formid[0]["ShortDescription"].value);
                tinymce.get("Description").setContent(formid[0]["Description"].value);
            };

            var afterShowForm = function (form) {
                tinymce.execCommand('mceAddEditor', false, 'ShortDescription');
                tinymce.execCommand('mceAddEditor', false, 'Description');
            };

            var onClose = function (form) {
                tinymce.execCommand('mceRemoveEditor', false, 'ShortDescription');
                tinymce.execCommand('mceRemoveEditor', false, 'Description');
            };

            var beforeSubmitHandler = function (postdata, form) {
                var selRowData = $(gridName).getRowData($(gridName).getGridParam('selrow'));
                if (selRowData["PostedOn"])
                    postdata.PostedOn = selRowData["PostedOn"];
                postdata.ShortDescription = tinymce.get("ShortDescription").getContent();
                postdata.Description = tinymce.get("Description").getContent();

                return [true];
            };

            // columns
            var colNames = [
                'Id',
                'Title',
                'Short Description',
                'Description',
                'Category',
                'Category',
                'Tags',
                'Meta',
                'Url Slug',
                'Published',
                'Posted On',
                'Modified'
            ];

            var columns = [];

            columns.push({
                name: 'Id',
                hidden: true,
                key: true
            });

            columns.push({
                name: 'Title',
                index: 'Title',
                width: 250,
                editable: true,
                editoptions: {
                    size: 43,
                    maxlength: 500
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'ShortDescription',
                width: 250,
                sortable: false,
                hidden: true,
                editable: true,
                edittype: 'textarea',
                editoptions: {
                    rows: "10",
                    cols: "100"
                },
                editrules: {
                    custom: true,
                    custom_func: function (val, colname) {
                        val = tinymce.get("ShortDescription").getContent();
                        if (val) return [true, ""];
                        return [false, colname + ": Field is required"];
                    },
                    edithidden: true
                }
            });

            columns.push({
                name: 'Description',
                width: 250,
                sortable: false,
                hidden: true,
                editable: true,
                edittype: 'textarea',
                editoptions: {
                    rows: "40",
                    cols: "100"
                },
                editrules: {
                    custom: true,

                    custom_func: function (val, colname) {
                        val = tinymce.get("Description").getContent();
                        if (val) return [true, ""];
                        return [false, colname + ": Field is requred"];
                    },
                    edithidden: true
                }
            });

            columns.push({
                name: 'Category.Id',
                hidden: true,
                editable: true,
                edittype: 'select',
                editoptions: {
                    style: 'width:250px;',
                    dataUrl: '/Admin/GetCategoriesHtml'
                },
                editrules: {
                    required: true,
                    edithidden: true
                }
            });

            columns.push({
                name: 'Category.Name',
                index: 'Category',
                width: 150
            });

            columns.push({
                name: 'Tags',
                width: 150,
                editable: true,
                edittype: 'select',
                editoptions: {
                    style: 'width:250px;',
                    dataUrl: '/Admin/GetTagsHtml',
                    multiple: true
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'Meta',
                width: 250,
                sortable: false,
                editable: true,
                edittype: 'textarea',
                editoptions: {
                    rows: "2",
                    cols: "40",
                    maxlength: 1000
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'UrlSlug',
                width: 200,
                sortable: false,
                editable: true,
                editoptions: {
                    size: 43,
                    maxlength: 200
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'Published',
                index: 'Published',
                width: 100,
                align: 'center',
                editable: true,
                edittype: 'checkbox',
                editoptions: {
                    value: "true:false",
                    defaultValue: 'false'
                }
            });

            columns.push({
                name: 'PostedOn',
                index: 'PostedOn',
                width: 150,
                align: 'center',
                sorttype: 'date',
                datefmt: 'm/d/Y'
            });

            columns.push({
                name: 'Modified',
                index: 'Modified',
                width: 100,
                align: 'center',
                sorttype: 'date',
                datefmt: 'm/d/Y'
            });

            // create the grid
            $(gridName).jqGrid({
                // server url and other ajax stuff
                url: '/Admin/Posts',
                datatype: 'json',
                mtype: 'GET',
                height: 'auto',
                toppager: true,

                // columns
                colNames: colNames,
                colModel: columns,

                // pagination options
                pager: pagerName,
                rowNum: 10,
                rowList: [10, 20, 30],

                // row number column
                rownumbers: true,
                rownumWidth: 40,

                // default sorting
                sortname: 'PostedOn',
                sortorder: 'desc',

                // display the no. of records message
                viewrecords: true,

                gridView: true,
                autoendcode: true,

                afterInsertRow: function (rowid, rowdata, rowelem) {
                    var tags = rowdata["Tags"];
                    var tagStr = "";

                    $.each(tags, function (i, t) {
                        if (tagStr) tagStr += ", "
                        tagStr += t.Name;
                    });


                    $(gridName).setRowData(rowid, { "Tags": tagStr });
                },

                jsonReader: { repeatitems: false }
            });

            // configuring add options
            var addOptions = {
                url: '/Admin/AddPost',
                addCaption: 'Add Post',
                processData: "Saving...",
                width: 1000,
                closeAfterAdd: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                bExit: 'Cancel',
                afterShowForm: afterShowForm,
                onClose: onClose,
                afterSubmit: LunarSFX.GridManager.afterSubmitHandler,
                beforeSubmit: beforeSubmitHandler
            };

            // configuring the edit options
            var editOptions = {
                url: '/Admin/EditPost',
                editCaption: 'Edit Post',
                processData: "Saving...",
                width: 1000,
                closeAfterEdit: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                bExit: 'Cancel',
                afterclickPgButtons: afterclickPgButtons,
                afterShowForm: afterShowForm,
                onClose: onClose,
                afterSubmit: LunarSFX.GridManager.afterSubmitHandler,
                beforeSubmit: beforeSubmitHandler
            };

            // configuring the delete options
            var deleteOptions = {
                url: '/Admin/DeletePost',
                caption: 'Delete Post',
                processData: "Saving...",
                msg: "Delete the Post?",
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Delete",
                bExit: 'Cancel',
                afterSubmit: LunarSFX.GridManager.afterSubmitHandler
            };

            $(gridName).navGrid(pagerName,
                                {
                                    addtext: 'add',
                                    addtitle: 'add blog post',
                                    deltext: 'delete',
                                    deltitle: 'delete blog post',
                                    edittext: 'edit',
                                    edittitle: 'edit blog post',
                                    refreshtext: 'refresh',
                                    refreshtitle: 'refresh list',
                                    cloneToTop: true,
                                    search: false
                                },
                                editOptions,
                                addOptions,
                                deleteOptions);
        },



        // function to create grid to manage categories
        categoriesGrid: function (gridName, pagerName) {
        },

        // function to create grid to manage tags
        tagsGrid: function (gridName, pagerName) {
        },

        afterSubmitHandler: function (response, postdata) {

            var json = $.parseJSON(response.responseText);

            if (json) return [json.success, json.message, json.id];

            return [false, "Failed to get result from server.", null];
        }
    };

    $("#tabs").tabs({
        create:
            function (event, ui) {
                var isLoaded = false;

                if (!isLoaded) {

                    var gdMgr = LunarSFX.GridManager, fn, gridName, pagerName;

                    switch (ui.tab.index()) {
                        case 0:
                        default:
                            fn = gdMgr.postsGrid;
                            gridName = "#tablePosts";
                            pagerName = "#pagerPosts";
                            break;
                        case 1:
                            fn = gdMgr.categoriesGrid;
                            gridName = "#tableCats";
                            pagerName = "#pagerCats";
                            break;
                        case 2:
                            fn = gdMgr.tagsGrid;
                            gridName = "#tableTags";
                            pagerName = "#pagerTags";
                            break;
                    };

                    fn(gridName, pagerName);
                    isLoaded = true;
                }
            },
        activate: function (event, ui) {
            var gdMgr = LunarSFX.GridManager;
            gdMgr.postsGrid;
        }
    });
}());
