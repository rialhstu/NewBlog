
var NewBlog = {}; 

NewBlog.GridManager= {

    postGrid:function(gridName, pagerName)
    {
        var afterclickPgButtons = function (whichbutton, formid, rowid) {
            tinyMCE.get("ShortDescription").setContent(formid[0]["ShortDescription"].value);
            tinyMCE.get("Description").setContent(formid[0]["Description"].value);
        };

        var afterShowForm = function (form) {
            tinyMCE.execCommand('mceAddControl', false, "ShortDescription");
            tinyMCE.execCommand('mceAddControl', false, "Description");
        };

        var onClose = function (form) {
            tinyMCE.execCommand('mceRemoveControl', false, "ShortDescription");
            tinyMCE.execCommand('mceRemoveControl', false, "Description");
        };



        var beforeSubmitHandler = function (postdata, form) {
            var selRowData = $(gridName).getRowData($(gridName).getGridParam('selrow'));
            if (selRowData["PostedDate"])
                postdata.PostedOn = selRowData["PostedDate"];
            postdata.ShortDescription = tinyMCE.get("ShortDescription").getContent();
            postdata.Description = tinyMCE.get("Description").getContent();

            return [true];
        };
          

        var colNames = [
       'Id',
       'TitleName',
       'ShortDescription',
       'Description',
       'cagegory',
       'cagegory',
       'tags',
       'Meta',
       'UrlName',
       'Published',
       'PostedDate',
       'Modified'
        ];

        var columns = [];

        columns.push({
            name: 'Id',
            hidden: true,
            key: true
        });

        columns.push({
            name: 'TitleName',
            index: 'TitleName',
            width: 250,
            editable: true,
            editoptions: {
                size: 43,
                maxlength: 500
            },
            editrules: {
                required: true
            },
            formatter: 'showlink',
            formatoptions: {
                target: "_new",
                baseLinkUrl: '/Add/GoToPost'
            }
        });

        columns.push({
            name: 'ShortDescription',
            index: 'ShortDescription',
            width: 250,
            editable: true,
            sortable: false,
            hidden: true,
            edittype: 'textarea',
            editoptions: {
                rows: "10",
                cols: "100"
            },
            editrules: {
                custom: true,
                custom_func: function (val, colname) {
                    val = tinyMCE.get("ShortDescription").getContent();
                    if (val) return [true, ""];
                    return [false, colname + ": Field is required"];
                },
                edithidden: true
            }
        });

        columns.push({
            name: 'Description',
            index: 'Description',
            width: 250,
            editable: true,
            sortable: false,
            hidden: true,
            edittype: 'textarea',
            
            editoptions: {
                rows: "40",
                cols: "100"
            },
            editrules: {
                custom: true,
                custom_func: function (val, colname) {
                    val = tinyMCE.get("Description").getContent();
                    if (val) return [true, ""];
                    return [false, colname + ": Field is requred"];
                },
                edithidden: true
            }
        });

        columns.push({
            name: 'cagegory.Id',
            hidden: true,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                dataUrl: '/Add/GetCategoriesHtml'
            },
            editrules: {
                required: true,
                edithidden: true
            }
        });

        columns.push({
            name: 'cagegory.Name',
            index: 'cagegory',
            width: 150
        });

        columns.push({
            name: 'tags',
            width: 150,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                dataUrl: '/Add/GetTagsHtml',
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
            name: 'UrlName',
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
            name: 'PostedDate',
            index: 'PostedDate',
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
            url: '/Add/Posts',
            datatype: 'json',
            mtype: 'GET',

            height: 'auto',
            toppager: true,

            // columns
            colNames: colNames,
            colModel: columns,

            // pagination options
            
            pager: pagerName,
            rownumbers: true,
            rownumWidth: 40,
            rowNum: 10,
            rowList: [10, 20, 30],

            // row number column
            

            // default sorting
            sortname: 'PostedDate',
            sortorder: 'desc',


            // display the no. of records message
            viewrecords: true,

            jsonReader: { repeatitems: false },

            afterInsertRow: function (rowid, rowdata, rowelem) {
                var tags = rowdata["tags"];
                var tagStr = "";

                $.each(tags, function (i, t) {
                    if (tagStr) tagStr += ", "
                    tagStr += t.Name;
                });


                $(gridName).setRowData(rowid, { "tags": tagStr });

            }
        });
        
      
        var addOptions = {
            url: '/Add/AddPost',
            addCaption: 'Add Post',
            processData: "Saving...",
            width: 900,
            closeAfterAdd: true,
            closeOnEscape: true,
            afterShowForm: afterShowForm,
            onClose: onClose,
        };
        var editOptions = {
            url: '/Add/EditPost',
            editCaption: 'Edit Post',
            processData: "Saving...",
            width: 900,
            closeAfterEdit: true,
            closeOnEscape: true,
            afterclickPgButtons: afterclickPgButtons,
            afterShowForm: afterShowForm,
            onClose: onClose,
            afterSubmit: NewBlog.GridManager.afterSubmitHandler,
            beforeSubmit: beforeSubmitHandler
        };
        var deleteOptions = {
            url: '/Add/DeletePost',
            caption: 'Delete Post',
            processData: "Saving...",
            msg: "Delete the Post?",
            closeOnEscape: true,
            afterSubmit: NewBlog.GridManager.afterSubmitHandler
        };
        


        $(gridName).navGrid(pagerName,
                        {
                            cloneToTop: true,
                            search: false
                        }, editOptions, addOptions, deleteOptions);


    },
    cateGoriesGrid:function(gridName, pagerName)
    {

        var colNames = ['Id', 'Name', 'UrlName', 'Description'];

        var columns = [];

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
            name: 'UrlName',
            index: 'UrlName',
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
            url: '/Add/Categories',
            datatype: 'json',
            mtype: 'GET',
            height: 'auto',
            toppager: true,
            colNames: colNames,
            colModel: columns,
            pager: pagerName,
            rownumbers: true,
            rownumWidth: 40,
            rowNum: 500,
            sortname: 'Name',
            loadonce: true,
            jsonReader: {
                repeatitems: false
            }
        });

        

        var editOptions = {
            url: '/Add/EditCategory',
            width: 400,
            editCaption: 'Edit Category',
            processData: "Saving...",
            closeAfterEdit: true,
            closeOnEscape: true,
            afterSubmit: function (response, postdata) {
                var json = $.parseJSON(response.responseText);

                if (json) {
                    $(gridName).jqGrid('setGridParam', { datatype: 'json' });
                    return [json.success, json.message, json.id];
                }

                return [false, "Failed to get result from server.", null];
            }
        };
        var addOptions = {
            url: '/Add/AddCategory',
            width: 400,
            addCaption: 'Add Category',
            processData: "Saving...",
            closeAfterAdd: true,
            closeOnEscape: true,
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
        var deleteOptions = {
            url: '/Add/DeleteCategory',
            caption: 'Delete Category',
            processData: "Saving...",
            width: 500,
            msg: "Delete the category? This will delete all the posts belongs to this category as well.",
            closeOnEscape: true,
            afterSubmit: NewBlog.GridManager.afterSubmitHandler
        };
        
        $(gridName).jqGrid('navGrid', pagerName,
        {
            cloneToTop: true,
            search: false
        },

        editOptions, addOptions, deleteOptions);


    },

    tagsgrid:function(gridName,pagerName)
    {
        var colNames = ['Id', 'Name', 'UrlName', 'Description'];

        var columns = [];

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
            name: 'UrlName',
            index: 'UrlName',
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
            url: '/Add/Tags',
            datatype: 'json',
            mtype: 'GET',
            height: 'auto',
            toppager: true,
            colNames: colNames,
            colModel: columns,
            pager: pagerName,
            rownumbers: true,
            rownumWidth: 40,
            rowNum: 500,
            sortname: 'Name',
            loadonce: true,
            jsonReader: {
                repeatitems: false
            }
        });

        var editOptions = {
            url: '/Add/EditTag',
            editCaption: 'Edit Tag',
            processData: "Saving...",
            closeAfterEdit: true,
            closeOnEscape: true,
            width: 400,
            afterSubmit: function (response, postdata) {
                var json = $.parseJSON(response.responseText);

                if (json) {
                    $(gridName).jqGrid('setGridParam', { datatype: 'json' });
                    return [json.success, json.message, json.id];
                }

                return [false, "Failed to get result from server.", null];
            }
        };

        var addOptions = {
            url: '/Add/AddTag',
            addCaption: 'Add Tag',
            processData: "Saving...",
            closeAfterAdd: true,
            closeOnEscape: true,
            width: 400,
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
            url: '/Add/DeleteTag',
            caption: 'Delete Tag',
            processData: "Saving...",
            width: 500,
            msg: "Delete the tag? This will delete all the posts belonged to this tag as well.",
            closeOnEscape: true,
            afterSubmit: NewBlog.GridManager.afterSubmitHandler
        };

        // configuring the navigation toolbar.
        $(gridName).jqGrid('navGrid', pagerName,
        {
            cloneToTop: true,
            search: false
        },

        editOptions, addOptions, deleteOptions);



        }
};

NewBlog.GridManager.afterSubmitHandler = function (response, postdata) {

    var json = $.parseJSON(response.responseText);

    if (json) return [json.success, json.message, json.id];

    return [false, "Failed to get result from server.", null];
};





$(function () {
    $("#tabs").tabs({
        show:function(event, ui)
        {
            if(!ui.tab.isLoaded)
            {
                var grd = NewBlog.GridManager, fn, gridName, pagerName;

                switch (ui.index) {
                    case 0:
                        fn = grd.postGrid;
                        gridName = "#tablePosts";
                        pagerName = "#pagerPosts";
                        break;
                    case 1:
                        fn = grd.cateGoriesGrid;
                        gridName = "#tableCats";
                        pagerName = "#pagerCats";
                        break;
                    case 2:
                        fn = grd.tagsgrid;
                        gridName = "#tableTags";
                        pagerName = "#pagerTags";
                        break;
                };
                fn(gridName, pagerName);
                ui.tab.isLoaded = true;
              




            }
        }
    }
        );
});