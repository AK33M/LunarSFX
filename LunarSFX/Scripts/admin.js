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

                shrinkToFit: true,

                gridview: false,
                autoendcode: true,

                afterInsertRow: function (rowid,rowdata, rowelem) {
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

            var colNames = ['Id', 'Name', 'Url Slug', 'Description'];

            var columns = [];

            var addOptions = {
                url: '/Admin/AddCategory',
                width: 400,
                addCaption: 'Add Category',
                processData: "Saving...",
                closeAfterAdd: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                bExit: 'Cancel',
                afterSubmit: function (response, postdata) {
                    var json = $.parseJSON(response.responseText);

                    if (json) {
                        // since the data is in the client-side, reload the grid.
                        $(gridName).jqGrid('setGridParam', { datatype: 'json' });
                        return [json.success, json.message, json.id];
                    }

                    return [false, "Failed to get result from server.", null];
                }
            };

            var editOptions = {
                url: '/Admin/EditCategory',
                width: 400,
                editCaption: 'Edit Category',
                processData: "Saving...",
                closeAfterEdit: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                bExit: 'Cancel',
                afterSubmit: function (response, postdata) {
                    var json = $.parseJSON(response.responseText);

                    if (json) {
                        $(gridName).jqGrid('setGridParam', { datatype: 'json' });
                        return [json.success, json.message, json.id];
                    }

                    return [false, "Failed to get result from server.", null];
                }
            };

            var deleteOptions = {
                url: '/Admin/DeleteCategory',
                caption: 'Delete Category',
                processData: "Saving...",
                width: 500,
                msg: "Delete the category? This will delete all the posts belongs to this category as well.",
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Delete",
                bExit: 'Cancel',
                afterSubmit: LunarSFX.GridManager.afterSubmitHandler
            };

            columns.push({
                name: 'Id',
                index: 'Id',
                hidden: true,
                sorttype: 'int',
                key: true,
                editable: false,
                editoptions: {
                    readonly: true
                }
            });

            columns.push({
                name: 'Name',
                index: 'Name',
                width: 200,
                editable: true,
                edittype: 'text',
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'UrlSlug',
                index: 'UrlSlug',
                width: 200,
                editable: true,
                edittype: 'text',
                sortable: false,
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'Description',
                index: 'Description',
                width: 200,
                editable: true,
                edittype: 'textarea',
                sortable: false,
                editoptions: {
                    rows: "4",
                    cols: "28"
                }
            });

            $(gridName).jqGrid({
                url: '/Admin/Categories',
                datatype: 'json',
                mtype: 'GET',
                height: 'auto',
                toppager: true,
                colNames: colNames,
                colModel: columns,
                pager: pagerName,
                shrinkToFit: true,
                width: 1000,
                rownumbers: true,
                rownumWidth: 40,
                rowNum: 500,
                sortname: 'Name',
                loadonce: true,
                jsonReader: {
                    repeatitems: false
                }
            });

            $(gridName).navGrid(pagerName,
                            { // settings
                                addtext: 'add',
                                addtitle: 'add category',
                                deltext: 'delete',
                                deltitle: 'delete category',
                                edittext: 'edit',
                                edittitle: 'edit category',
                                refreshtext: 'refresh',
                                refreshtitle: 'refresh list',
                                cloneToTop: true,
                                search: false
                            },
                            editOptions, // edit options
                            addOptions, // add options
                            deleteOptions// delete options
                        );
        },

        // function to create grid to manage tags
        tagsGrid: function (gridName, pagerName) {
            var colNames = ['Id', 'Name', 'Url Slug', 'Description'];

            var columns = [];

            var addOptions = {
                url: '/Admin/AddTag',
                width: 400,
                addCaption: 'Add Tag',
                processData: "Saving...",
                closeAfterAdd: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                bExit: 'Cancel',
                afterSubmit: function (response, postdata) {
                    var json = $.parseJSON(response.responseText);

                    if (json) {
                        $(gridName).jqGrid('setGridParam', { datatype: 'json' });
                        return [json.success, json.message, json.id];
                    }

                    return [false, "Failed to get result from server.", null];
                }
            };

            var editOptions = {
                url: '/Admin/EditTag',
                width: 400,
                editCaption: 'Edit Tag',
                processData: "Saving...",
                closeAfterEdit: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                bExit: 'Cancel',
                afterSubmit: function (response, postdata) {
                    var json = $.parseJSON(response.responseText);

                    if (json) {
                        $(gridName).jqGrid('setGridParam', { datatype: 'json' });
                        return [json.success, json.message, json.id];
                    }

                    return [false, "Failed to get result from server.", null];
                }
            };

            var deleteOptions = {
                url: '/Admin/DeleteTag',
                caption: 'Delete Tag',
                processData: "Saving...",
                width: 400,
                msg: "Delete the tag? This will delete all the posts belongs to this tag as well.",
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Delete",
                bExit: 'Cancel',
                afterSubmit: LunarSFX.GridManager.afterSubmitHandler
            };

            columns.push({
                name: 'Id',
                index: 'Id',
                hidden: true,
                sorttype: 'int',
                key: true,
                editable: false,
                editoptions: {
                    readonly: true
                }
            });

            columns.push({
                name: 'Name',
                index: 'Name',
                width: 200,
                editable: true,
                edittype: 'text',
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'UrlSlug',
                index: 'UrlSlug',
                width: 200,
                editable: true,
                edittype: 'text',
                sortable: false,
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'Description',
                index: 'Description',
                width: 200,
                editable: true,
                edittype: 'textarea',
                sortable: false,
                editoptions: {
                    rows: "4",
                    cols: "28"
                }
            });

            $(gridName).jqGrid({
                url: '/Admin/Tags',
                datatype: 'json',
                mtype: 'GET',
                height: 'auto',
                toppager: true,
                colNames: colNames,
                colModel: columns,
                pager: pagerName,
                shrinkToFit: true,
                width: 1000,
                rownumbers: true,
                rownumWidth: 40,
                rowNum: 500,
                sortname: 'Name',
                loadonce: true,
                jsonReader: {
                    repeatitems: false
                }
            });

            // configuring the navigation toolbar.
            $(gridName).jqGrid('navGrid', pagerName,
            {
                addtext: 'add',
                addtitle: 'add tag',
                deltext: 'delete',
                deltitle: 'delete tag',
                edittext: 'edit',
                edittitle: 'edit tag',
                refreshtext: 'refresh',
                refreshtitle: 'refresh list',
                cloneToTop: true,
                search: false
            },

            editOptions, addOptions, deleteOptions);
        },

        // function to create grid to manage users
        usersGrid: function (gridName, pagerName) {
            var colNames = ['Id', 'Email', 'UserName', 'Roles'];

            var columns = [];

            var editOptions = {
                url: '/Admin/EditUser',
                width: 400,
                editCaption: 'Edit User',
                processData: "Saving...",
                closeAfterEdit: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                bExit: 'Cancel',
                afterSubmit: function (response, postdata) {
                    var json = $.parseJSON(response.responseText);

                    if (json) {
                        $(gridName).jqGrid('setGridParam', { datatype: 'json' });
                        return [json.success, json.message, json.id];
                    }

                    return [false, "Failed to get result from server.", null];
                }
            };

            var deleteOptions = {
                url: '/Admin/EditUser',
                caption: 'Delete User',
                processData: "Saving...",
                width: 400,
                msg: "Delete this user?",
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Delete",
                bExit: 'Cancel',
                afterSubmit: LunarSFX.GridManager.afterSubmitHandler
            };

            columns.push({
                name: 'Id',
                index: 'Id',
                hidden: true,
                sorttype: 'int',
                key: true,
                editable: false,
                editoptions: {
                    readonly: true
                }
            });

            columns.push({
                name: 'Email',
                index: 'Email',
                width: 200,
                editable: true,
                edittype: 'text',
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'UserName',
                index: 'UserName',
                width: 200,
                editable: true,
                edittype: 'text',
                sortable: false,
                editoptions: {
                    size: 30,
                    maxlength: 50
                },
                editrules: {
                    required: true
                }
            });

            columns.push({
                name: 'Roles',
                width: 150,
                editable: true,
                edittype: 'select',
                editoptions: {
                    style: 'width:250px;',
                    dataUrl: '/Admin/GetRolesHtml',
                    multiple: true
                },
                editrules: {
                    required: true
                }
            });

            //create the grid
            $(gridName).jqGrid({
                url: '/Admin/Users',
                datatype: 'json',
                mtype: 'GET',
                height: 'auto',
                toppager: true,
                colNames: colNames,
                colModel: columns,
                pager: pagerName,
                shrinkToFit: true,
                width: 1000,
                rownumbers: true,
                rownumWidth: 40,
                rowNum: 500,
                sortname: 'Id',
                loadonce: true,
                jsonReader: {
                    repeatitems: false
                }
            });

            // configuring the navigation toolbar.
            $(gridName).jqGrid('navGrid', pagerName,
                {
                    add: false,
                    deltext: 'delete',
                    deltitle: 'delete user',
                    edittext: 'edit',
                    edittitle: 'edit user',
                    refreshtext: 'refresh',
                    refreshtitle: 'refresh list',
                    cloneToTop: true,
                    search: false
                },

                editOptions, {}, deleteOptions);

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
                var gdMgr = LunarSFX.GridManager, fn, gridName, pagerName;
                fn = gdMgr.postsGrid;
                gridName = "#tablePosts";
                pagerName = "#pagerPosts";

                fn(gridName, pagerName);
            },

        activate: function (event, ui) {
            var gdMgr = LunarSFX.GridManager, fn, gridName, pagerName;
            switch (ui.newTab.index()) {
                case 0:
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
                case 3:
                    fn = gdMgr.usersGrid;
                    gridName = "#tableUsers";
                    pagerName = "#pagerUsers";
                    break;
            };

            fn(gridName, pagerName);
        }
    });
});