$(function () {
    var LunarSFX = {};

    LunarSFX.GridManager = {

        // function to create grid to manage posts
        postsGrid: function (gridName, pagerName) {
        },

        // function to create grid to manage categories
        categoriesGrid: function (gridName, pagerName) {
        },

        // function to create grid to manage tags
        tagsGrid: function (gridName, pagerName) {
        }
    };

    $("#tabs").tabs({
        show: function (event, ui) {

            if (!ui.tab.isLoaded) {

                var gdMgr = LunarSFX.GridManager,
                    fn, gridName, pagerName;

                switch (ui.index) {
                    case 0:
                        // call function to create grid for managing posts
                        // from "#tablePosts" and "#pagerPosts"
                        fn.gdMgr.postsGrid;
                        gridName = "#tablePosts";
                        pagerName = "#pagerPosts";
                        break;
                    case 1:
                        // call function to create grid for managing posts
                        // from "#tableCats" and "#pagerCats"
                        fn.gdMgr.categoriesGrid;
                        gridName = "#tableCats";
                        pagerName = "#pagerCats";
                        break;
                    case 2:
                        // call function to create grid for managing posts
                        // from "#tableTags" and "#pagerTags";
                        fn.gdMgr.tagsGrid;
                        gridName = "#tableTags";
                        pagerName = "#pagerTags";
                        break;
                };

                fn(gridName, pagerName);
                ui.tab.isLoaded = true;
            }
        }
    });
});
