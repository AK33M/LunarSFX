$(function () {

    var LunarSFX = {};

    LunarSFX.GridManager = {

        // function to create grid to manage posts
        postsGrid: function (gridName, pagerName) {
            console.log(gridName);
        },

        // function to create grid to manage categories
        categoriesGrid: function (gridName, pagerName) {
            console.log(gridName);
        },

        // function to create grid to manage tags
        tagsGrid: function (gridName, pagerName) {
            console.log(gridName);
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
            var gdMgr = LunarSFX.GridManager;
            switch (ui.newTab.index()) {
                case 0:
                    fn = gdMgr.postsGrid, fn, gridName, pagerName;
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
        }
    });
});