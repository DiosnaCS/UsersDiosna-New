(function ($) {
    function CreateArticle() {
        var $this = this;

        function initialize() {
            $('#Text').summernote({
                focus: true,
                height: 150,  
                codemirror: { 
                    theme: 'united'
                }
            });
        }

        $this.init = function () {
            initialize();
        }
    }
    $(function () {
        var self = new CreateArticle();
        self.init();
    })
}(jQuery))