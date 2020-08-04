// TRANSLATION
// -----------------------------------

(function() {
    'use strict';

    $(initTranslation);


    var pathPrefix = '/i18n'; // folder of json files
    var STORAGEKEY = 'jq-appLang';
    var savedLanguage = Storages.localStorage.get(STORAGEKEY);

    function initTranslation() {
        i18next
            .use(i18nextXHRBackend)
            // .use(LanguageDetector)
            .init({
                    fallbackLng: savedLanguage || 'es',
                    backend: {
                        loadPath: pathPrefix + '/{{ns}}-{{lng}}.json',
                    },
                    ns: ['site'],
                    defaultNS: 'site',
                    debug: false
                },
                function(err, t) {
                    // initialize elements
                    applyTranlations();
                    // listen to language changes
                    attachChangeListener();
                });

        function applyTranlations() {
            var list = [].slice.call(document.querySelectorAll('[data-localize]'));
            list.forEach(function(item) {
                var key = item.getAttribute('data-localize');
                if (i18next.exists(key)) item.innerHTML = i18next.t(key);
            });
        }

        function attachChangeListener() {
            var list = [].slice.call(document.querySelectorAll('[data-set-lang]'));
            list.forEach(function(item) {

                item.addEventListener('click',
                    function(e) {
                        if (e.target.tagName === 'A') e.preventDefault();
                        var lang = item.getAttribute('data-set-lang');
                        if (lang) {
                            i18next.changeLanguage(lang,
                                function(err) {
                                    if (err) console.log(err);
                                    else {
                                        applyTranlations();
                                        Storages.localStorage.set(STORAGEKEY, lang);
                                    }
                                });
                        }
                        activateDropdown(item);
                    });

            });
        }

        function activateDropdown(item) {
            if (item.classList.contains('dropdown-item')) {
                item.parentElement.previousElementSibling.innerHTML = item.innerHTML;
            }
        }

    }


})();