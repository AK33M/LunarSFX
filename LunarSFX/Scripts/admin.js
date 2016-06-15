$(function () {

    var LunarSFX = {};

    LunarSFX.GridManager = {

        // function to create grid to manage posts
        postsGrid: function(gridName, pagerName) {
 
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
 
                    $.each(tags, function(i, t){
                        if(tagStr) tagStr += ", "
                        tagStr += t.Name;
                    });
 
 
                    $(gridName).setRowData(rowid, { "Tags": tagStr });
                },
 
                jsonReader: { repeatitems: false }
            });

            var afterShowForm = function (form) {
                tinyMCE.execCommand('mceAddControl', false, 'ShortDescription');
                tinyMCE.execCommand('mceAddControl', false, 'Description');
            };

            var onClose = function (form) {
                tinyMCE.execCommand('mceRemoveControl', false, 'ShortDescription');
                tinyMCE.execCommand('mceRemoveControl', false, 'Description');
            };

            // configuring add options
            var addOptions = {
                url: '/Admin/AddPost',
                addCaption: 'Add Post',
                processData: "Saving...",
                width: 900,
                closeAfterAdd: true,
                closeOnEscape: true,
                bCancel: "Cancel",
                bSubmit: "Submit",
                afterShowForm: afterShowForm,
                onClose: onClose,
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
                                {},
                                addOptions,
                                {});
        },

        // function to create grid to manage categories
        categoriesGrid: function (gridName, pagerName) {
        },

        // function to create grid to manage tags
        tagsGrid: function (gridName, pagerName) {
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
        }
    });   
}());
