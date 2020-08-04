/**
 * This library was created to emulate some jQuery features
 * used in this template only with Javascript and DOM
 * manipulation functions (IE10+).
 * All methods were designed for an adequate and specific use
 * and don't perform a deep validation on the arguments provided.
 *
 * IMPORTANT:
 * ==========
 * It's suggested NOT to use this library extensively unless you
 * understand what each method does. Instead, use only JS or
 * you might even need jQuery.
 */

(function(global, factory) {
    if (typeof exports === 'object') { // CommonJS-like
        module.exports = factory();
    } else { // Browser
        if (typeof global.jQuery === 'undefined')
            global.$ = factory();
    }
}(this, function() {

    // HELPERS
    function arrayFrom(obj) {
        return ('length' in obj) && (obj !== window) ? [].slice.call(obj) : [obj];
    }

    function filter(ctx, fn) {
        return [].filter.call(ctx, fn);
    }

    function map(ctx, fn) {
        return [].map.call(ctx, fn);
    }

    function matches(item, selector) {
        return (Element.prototype.matches || Element.prototype.msMatchesSelector).call(item, selector)
    }

    // Events handler with simple scoped events support
    var EventHandler = function() {
        this.events = {};
    }
    EventHandler.prototype = {
        // event accepts: 'click' or 'click.scope'
        bind: function(event, listener, target) {
            var type = event.split('.')[0];
            target.addEventListener(type, listener, false);
            this.events[event] = {
                type: type,
                listener: listener
            }
        },
        unbind: function(event, target) {
            if (event in this.events) {
                target.removeEventListener(this.events[event].type, this.events[event].listener, false);
                delete this.events[event];
            }
        }
    }

    // Object Definition
    var Wrap = function(selector) {
        this.selector = selector;
        return this._setup([]);
    }

    // CONSTRUCTOR
    Wrap.Constructor = function(param, attrs) {
        var el = new Wrap(param);
        return el.init(attrs);
    };

    // Core methods
    Wrap.prototype = {
        constructor: Wrap,
        init: function(attrs) {
            // empty object
            if (!this.selector) return this;
            // selector === string
            if (typeof this.selector === 'string') {
                // if looks like markup, try to create an element
                if (this.selector[0] === '<') {
                    var elem = this._setup([this._create(this.selector)])
                    return attrs ? elem.attr(attrs) : elem;
                } else
                    return this._setup(arrayFrom(document.querySelectorAll(this.selector)))
            }
            // selector === DOMElement
            if (this.selector.nodeType)
                return this._setup([this.selector])
            else // shorthand for DOMReady
                if (typeof this.selector === 'function')
                    return this._setup([document]).ready(this.selector)
            // Array like objects (e.g. NodeList/HTMLCollection)
            return this._setup(arrayFrom(this.selector))
        },
        /**
         * Creates a DOM element from a string
         * Strictly supports the form: '<tag>' or '<tag/>'
         */
        _create: function(str) {
            var nodeName = str.substr(str.indexOf('<') + 1, str.indexOf('>') - 1).replace('/', '');
            return document.createElement(nodeName);
        },
        /** setup properties and array to element set */
        _setup: function(elements) {
            var i = 0;
            for (; i < elements.length; i++) delete this[i]; // clean up old set
            this.elements = elements;
            this.length = elements.length;
            for (i = 0; i < elements.length; i++) this[i] = elements[i] // new set
            return this;
        },
        _first: function(cb, ret) {
            var f = this.elements[0];
            return f ? (cb ? cb.call(this, f) : f) : ret;
        },
        /** Common function for class manipulation  */
        _classes: function(method, classname) {
            var cls = classname.split(' ');
            if (cls.length > 1) {
                cls.forEach(this._classes.bind(this, method));
            } else {
                if (method === 'contains') {
                    var elem = this._first();
                    return elem ? elem.classList.contains(classname) : false;
                }
                return (classname === '')
                    ? this
                    : this.each(function(i, item) {
                        item.classList[method](classname);
                    });
            }
        },
        /**
         * Multi purpose function to set or get a (key, value)
         * If no value, works as a getter for the given key
         * key can be an object in the form {key: value, ...}
         */
        _access: function(key, value, fn) {
            if (typeof key === 'object') {
                for (var k in key) {
                    this._access(k, key[k], fn);
                }
            } else if (value === undefined) {
                return this._first(function(elem) {
                    return fn(elem, key);
                });
            }
            return this.each(function(i, item) {
                fn(item, key, value);
            });
        },
        each: function(fn, arr) {
            arr = arr ? arr : this.elements;
            for (var i = 0; i < arr.length; i++) {
                if (fn.call(arr[i], i, arr[i]) === false)
                    break;
            }
            return this;
        }
    }

    /** Allows to extend with new methods */
    Wrap.extend = function(methods) {
        Object.keys(methods).forEach(function(m) {
            Wrap.prototype[m] = methods[m]
        })
    }

    // DOM READY
    Wrap.extend({
        ready: function(fn) {
            if (document.attachEvent ? document.readyState === 'complete' : document.readyState !== 'loading') {
                fn();
            } else {
                document.addEventListener('DOMContentLoaded', fn);
            }
            return this;
        }
    })
    // ACCESS
    Wrap.extend({
        /** Get or set a css value */
        css: function(key, value) {
            var getStyle = function(e, k) { return e.style[k] || getComputedStyle(e)[k]; };
            return this._access(key, value, function(item, k, val) {
                var unit = (typeof val === 'number') ? 'px' : '';
                return val === undefined ? getStyle(item, k) : (item.style[k] = val + unit);
            })
        },
        /** Get an attribute or set it */
        attr: function(key, value) {
            return this._access(key, value, function(item, k, val) {
                return val === undefined ? item.getAttribute(k) : item.setAttribute(k, val)
            })
        },
        /** Get a property or set it */
        prop: function(key, value) {
            return this._access(key, value, function(item, k, val) {
                return val === undefined ? item[k] : (item[k] = val);
            })
        },
        position: function() {
            return this._first(function(elem) {
                return { left: elem.offsetLeft, top: elem.offsetTop }
            });
        },
        scrollTop: function(value) {
            return this._access('scrollTop', value, function(item, k, val) {
                return val === undefined ? item[k] : (item[k] = val);
            })
        },
        outerHeight: function(includeMargin) {
            return this._first(function(elem) {
                var style = getComputedStyle(elem);
                var margins = includeMargin ? (parseInt(style.marginTop, 10) + parseInt(style.marginBottom, 10)) : 0;
                return elem.offsetHeight + margins;
            });
        },
        /**
         * Find the position of the first element in the set
         * relative to its sibling elements.
         */
        index: function() {
            return this._first(function(el) {
                return arrayFrom(el.parentNode.children).indexOf(el)
            }, -1);
        }
    })
    // LOOKUP
    Wrap.extend({
        children: function(selector) {
            var childs = [];
            this.each(function(i, item) {
                childs = childs.concat(map(item.children, function(item) {
                    return item
                }))
            })
            return Wrap.Constructor(childs).filter(selector);
        },
        siblings: function() {
            var sibs = []
            this.each(function(i, item) {
                sibs = sibs.concat(filter(item.parentNode.children, function(child) {
                    return child !== item;
                }))
            })
            return Wrap.Constructor(sibs)
        },
        /** Return the parent of each element in the current set */
        parent: function() {
            var par = map(this.elements, function(item) {
                return item.parentNode;
            })
            return Wrap.Constructor(par)
        },
        /** Return ALL parents of each element in the current set */
        parents: function(selector) {
            var par = [];
            this.each(function(i, item) {
                for (var p = item.parentElement; p; p = p.parentElement)
                    par.push(p);
            })
            return Wrap.Constructor(par).filter(selector)
        },
        /**
         * Get the descendants of each element in the set, filtered by a selector
         * Selector can't start with ">" (:scope not supported on IE).
         */
        find: function(selector) {
            var found = []
            this.each(function(i, item) {
                found = found.concat(map(item.querySelectorAll( /*':scope ' + */ selector), function(fitem) {
                    return fitem
                }))
            })
            return Wrap.Constructor(found)
        },
        /** filter the actual set based on given selector */
        filter: function(selector) {
            if (!selector) return this;
            var res = filter(this.elements, function(item) {
                return matches(item, selector)
            })
            return Wrap.Constructor(res)
        },
        /** Works only with a string selector */
        is: function(selector) {
            var found = false;
            this.each(function(i, item) {
                return !(found = matches(item, selector))
            })
            return found;
        }
    });
    // ELEMENTS
    Wrap.extend({
        /**
         * append current set to given node
         * expects a dom node or set
         * if element is a set, prepends only the first
         */
        appendTo: function(elem) {
            elem = elem.nodeType ? elem : elem._first()
            return this.each(function(i, item) {
                elem.appendChild(item);
            })
        },
        /**
         * Append a domNode to each element in the set
         * if element is a set, append only the first
         */
        append: function(elem) {
            elem = elem.nodeType ? elem : elem._first()
            return this.each(function(i, item) {
                item.appendChild(elem);
            })
        },
        /**
         * Insert the current set of elements after the element
         * that matches the given selector in param
         */
        insertAfter: function(selector) {
            var target = document.querySelector(selector);
            return this.each(function(i, item) {
                target.parentNode.insertBefore(item, target.nextSibling);
            })
        },
        /**
         * Clones all element in the set
         * returns a new set with the cloned elements
         */
        clone: function() {
            var clones = map(this.elements, function(item) {
                return item.cloneNode(true)
            })
            return Wrap.Constructor(clones);
        },
        /** Remove all node in the set from DOM. */
        remove: function() {
            this.each(function(i, item) {
                delete item.events;
                delete item.data;
                if (item.parentNode) item.parentNode.removeChild(item);
            })
            this._setup([])
        }
    })
    // DATASETS
    Wrap.extend({
        /**
         * Expected key in camelCase format
         * if value provided save data into element set
         * if not, return data for the first element
         */
        data: function(key, value) {
            var hasJSON = /^(?:\{[\w\W]*\}|\[[\w\W]*\])$/,
                dataAttr = 'data-' + key.replace(/[A-Z]/g, '-$&').toLowerCase();
            if (value === undefined) {
                return this._first(function(el) {
                    if (el.data && el.data[key])
                        return el.data[key];
                    else {
                        var data = el.getAttribute(dataAttr)
                        if (data === 'true') return true;
                        if (data === 'false') return false;
                        if (data === +data + '') return +data;
                        if (hasJSON.test(data)) return JSON.parse(data);
                        return data;
                    }
                });
            } else {
                return this.each(function(i, item) {
                    item.data = item.data || {};
                    item.data[key] = value;
                });
            }
        }
    })
    // EVENTS
    Wrap.extend({
        trigger: function(type) {
            type = type.split('.')[0]; // ignore namespace
            var event = document.createEvent('HTMLEvents');
            event.initEvent(type, true, false);
            return this.each(function(i, item) {
                item.dispatchEvent(event);
            })
        },
        blur: function() {
            return this.trigger('blur')
        },
        focus: function() {
            return this.trigger('focus')
        },
        on: function(event, callback) {
            return this.each(function(i, item) {
                if (!item.events) item.events = new EventHandler();
                event.split(' ').forEach(function(ev) {
                    item.events.bind(ev, callback, item);
                })
            })
        },
        off: function(event) {
            return this.each(function(i, item) {
                if (item.events) {
                    item.events.unbind(event, item);
                    delete item.events;
                }
            })
        }
    })
    // CLASSES
    Wrap.extend({
        toggleClass: function(classname) {
            return this._classes('toggle', classname);
        },
        addClass: function(classname) {
            return this._classes('add', classname);
        },
        removeClass: function(classname) {
            return this._classes('remove', classname);
        },
        hasClass: function(classname) {
            return this._classes('contains', classname);
        }
    })


    /**
     * Some basic features in this template relies on Bootstrap
     * plugins, like Collapse, Dropdown and Tab.
     * Below code emulates plugins behavior by toggling classes
     * from elements to allow a minimum interaction without animation.
     * - Only Collapse is required which is used by the sidebar.
     * - Tab and Dropdown are optional features.
     */

    // Emulate jQuery symbol to simplify usage
    var $ = Wrap.Constructor;

    // Emulates Collapse plugin
    Wrap.extend({
        collapse: function(action) {
            return this.each(function(i, item) {
                var $item = $(item).trigger(action + '.bs.collapse');
                if (action === 'toggle') $item.collapse($item.hasClass('show') ? 'hide' : 'show');
                else $item[action === 'show' ? 'addClass' : 'removeClass']('show');
            })
        }
    })
    // Initializations
    $('[data-toggle]').on('click', function(e) {
        var target = $(e.currentTarget);
        if (target.is('a')) e.preventDefault();
        switch (target.data('toggle')) {
            case 'collapse':
                $(target.attr('href')).collapse('toggle');
                break;
            case 'tab':
                target.parent().parent().find('.active').removeClass('active');
                target.addClass('active');
                var tabPane = $(target.attr('href'));
                tabPane.siblings().removeClass('active show');
                tabPane.addClass('active show');
                break;
            case 'dropdown':
                var dd = target.parent().toggleClass('show');
                dd.find('.dropdown-menu').toggleClass('show');
                break;
            default:
                break;
        }
    })


    return Wrap.Constructor

}));
/*!
 *
 * Mersy - Sistema de Registros Medicos
 *
 * Version: 4.2.2
 * Author: @themicon_co
 * Website: http://themicon.co
 * License: https://wrapbootstrap.com/help/licenses
 *
 */


(function() {
    'use strict';

    $(function() {

        // Restore body classes
        // -----------------------------------
        var $body = $('body');
        new StateToggler().restoreState($body);

        // enable settings toggle after restore
        $('#chk-fixed').prop('checked', $body.hasClass('layout-fixed'));
        $('#chk-collapsed').prop('checked', $body.hasClass('aside-collapsed'));
        $('#chk-collapsed-text').prop('checked', $body.hasClass('aside-collapsed-text'));
        $('#chk-boxed').prop('checked', $body.hasClass('layout-boxed'));
        $('#chk-float').prop('checked', $body.hasClass('aside-float'));
        $('#chk-hover').prop('checked', $body.hasClass('aside-hover'));

        // When ready display the offsidebar
        $('.offsidebar.d-none').removeClass('d-none');

    }); // doc ready

})();
// Knob chart
// -----------------------------------

(function() {
    'use strict';

    $(initKnob);

    function initKnob() {

        if (!$.fn.knob) return;

        var knobLoaderOptions1 = {
            width: '50%', // responsive
            displayInput: true,
            fgColor: APP_COLORS['info']
        };
        $('#knob-chart1').knob(knobLoaderOptions1);

        var knobLoaderOptions2 = {
            width: '50%', // responsive
            displayInput: true,
            fgColor: APP_COLORS['purple'],
            readOnly: true
        };
        $('#knob-chart2').knob(knobLoaderOptions2);

        var knobLoaderOptions3 = {
            width: '50%', // responsive
            displayInput: true,
            fgColor: APP_COLORS['info'],
            bgColor: APP_COLORS['gray'],
            angleOffset: -125,
            angleArc: 250
        };
        $('#knob-chart3').knob(knobLoaderOptions3);

        var knobLoaderOptions4 = {
            width: '50%', // responsive
            displayInput: true,
            fgColor: APP_COLORS['pink'],
            displayPrevious: true,
            thickness: 0.1,
            lineCap: 'round'
        };
        $('#knob-chart4').knob(knobLoaderOptions4);

    }

})();
// Chart JS
// -----------------------------------

(function() {
    'use strict';

    $(initChartJS);

    function initChartJS() {

        if (typeof Chart === 'undefined') return;

        // random values for demo
        var rFactor = function() {
            return Math.round(Math.random() * 100);
        };

        // Line chart
        // -----------------------------------

        var lineData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [{
                label: 'My First dataset',
                backgroundColor: 'rgba(114,102,186,0.2)',
                borderColor: 'rgba(114,102,186,1)',
                pointBorderColor: '#fff',
                data: [rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor()]
            }, {
                label: 'My Second dataset',
                backgroundColor: 'rgba(35,183,229,0.2)',
                borderColor: 'rgba(35,183,229,1)',
                pointBorderColor: '#fff',
                data: [rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor()]
            }]
        };

        var lineOptions = {
            legend: {
                display: false
            }
        };
        var linectx = document.getElementById('chartjs-linechart').getContext('2d');
        var lineChart = new Chart(linectx, {
            data: lineData,
            type: 'line',
            options: lineOptions
        });

        // Bar chart
        // -----------------------------------

        var barData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [{
                backgroundColor: '#23b7e5',
                borderColor: '#23b7e5',
                data: [rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor()]
            }, {
                backgroundColor: '#5d9cec',
                borderColor: '#5d9cec',
                data: [rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor(), rFactor()]
            }]
        };

        var barOptions = {
            legend: {
                display: false
            }
        };
        var barctx = document.getElementById('chartjs-barchart').getContext('2d');
        var barChart = new Chart(barctx, {
            data: barData,
            type: 'bar',
            options: barOptions
        });

        //  Doughnut chart
        // -----------------------------------

        var doughnutData = {
            labels: [
                'Purple',
                'Yellow',
                'Blue'
            ],
            datasets: [{
                data: [300, 50, 100],
                backgroundColor: [
                    '#7266ba',
                    '#fad732',
                    '#23b7e5'
                ],
                hoverBackgroundColor: [
                    '#7266ba',
                    '#fad732',
                    '#23b7e5'
                ]
            }]
        };

        var doughnutOptions = {
            legend: {
                display: false
            }
        };
        var doughnutctx = document.getElementById('chartjs-doughnutchart').getContext('2d');
        var doughnutChart = new Chart(doughnutctx, {
            data: doughnutData,
            type: 'doughnut',
            options: doughnutOptions
        });

        // Pie chart
        // -----------------------------------

        var pieData = {
            labels: [
                'Purple',
                'Yellow',
                'Blue'
            ],
            datasets: [{
                data: [300, 50, 100],
                backgroundColor: [
                    '#7266ba',
                    '#fad732',
                    '#23b7e5'
                ],
                hoverBackgroundColor: [
                    '#7266ba',
                    '#fad732',
                    '#23b7e5'
                ]
            }]
        };

        var pieOptions = {
            legend: {
                display: false
            }
        };
        var piectx = document.getElementById('chartjs-piechart').getContext('2d');
        var pieChart = new Chart(piectx, {
            data: pieData,
            type: 'pie',
            options: pieOptions
        });

        // Polar chart
        // -----------------------------------

        var polarData = {
            datasets: [{
                data: [
                    11,
                    16,
                    7,
                    3
                ],
                backgroundColor: [
                    '#f532e5',
                    '#7266ba',
                    '#f532e5',
                    '#7266ba'
                ],
                label: 'My dataset' // for legend
            }],
            labels: [
                'Label 1',
                'Label 2',
                'Label 3',
                'Label 4'
            ]
        };

        var polarOptions = {
            legend: {
                display: false
            }
        };
        var polarctx = document.getElementById('chartjs-polarchart').getContext('2d');
        var polarChart = new Chart(polarctx, {
            data: polarData,
            type: 'polarArea',
            options: polarOptions
        });

        // Radar chart
        // -----------------------------------

        var radarData = {
            labels: ['Eating', 'Drinking', 'Sleeping', 'Designing', 'Coding', 'Cycling', 'Running'],
            datasets: [{
                label: 'My First dataset',
                backgroundColor: 'rgba(114,102,186,0.2)',
                borderColor: 'rgba(114,102,186,1)',
                data: [65, 59, 90, 81, 56, 55, 40]
            }, {
                label: 'My Second dataset',
                backgroundColor: 'rgba(151,187,205,0.2)',
                borderColor: 'rgba(151,187,205,1)',
                data: [28, 48, 40, 19, 96, 27, 100]
            }]
        };

        var radarOptions = {
            legend: {
                display: false
            }
        };
        var radarctx = document.getElementById('chartjs-radarchart').getContext('2d');
        var radarChart = new Chart(radarctx, {
            data: radarData,
            type: 'radar',
            options: radarOptions
        });

    }

})();
// Chartist
// -----------------------------------

(function() {
    'use strict';

    $(initChartists);

    function initChartists() {

        if (typeof Chartist === 'undefined') return;

        // Bar bipolar
        // -----------------------------------
        var data1 = {
            labels: ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'W8', 'W9', 'W10'],
            series: [
                [1, 2, 4, 8, 6, -2, -1, -4, -6, -2]
            ]
        };

        var options1 = {
            high: 10,
            low: -10,
            height: 280,
            axisX: {
                labelInterpolationFnc: function(value, index) {
                    return index % 2 === 0 ? value : null;
                }
            }
        };

        new Chartist.Bar('#ct-bar1', data1, options1);

        // Bar Horizontal
        // -----------------------------------
        new Chartist.Bar('#ct-bar2', {
            labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            series: [
                [5, 4, 3, 7, 5, 10, 3],
                [3, 2, 9, 5, 4, 6, 4]
            ]
        }, {
            seriesBarDistance: 10,
            reverseData: true,
            horizontalBars: true,
            height: 280,
            axisY: {
                offset: 70
            }
        });

        // Line
        // -----------------------------------
        new Chartist.Line('#ct-line1', {
            labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
            series: [
                [12, 9, 7, 8, 5],
                [2, 1, 3.5, 7, 3],
                [1, 3, 4, 5, 6]
            ]
        }, {
            fullWidth: true,
            height: 280,
            chartPadding: {
                right: 40
            }
        });


        // SVG Animation
        // -----------------------------------

        var chart1 = new Chartist.Line('#ct-line3', {
            labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            series: [
                [1, 5, 2, 5, 4, 3],
                [2, 3, 4, 8, 1, 2],
                [5, 4, 3, 2, 1, 0.5]
            ]
        }, {
            low: 0,
            showArea: true,
            showPoint: false,
            fullWidth: true,
            height: 300
        });

        chart1.on('draw', function(data) {
            if (data.type === 'line' || data.type === 'area') {
                data.element.animate({
                    d: {
                        begin: 2000 * data.index,
                        dur: 2000,
                        from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                        to: data.path.clone().stringify(),
                        easing: Chartist.Svg.Easing.easeOutQuint
                    }
                });
            }
        });


        // Slim animation
        // -----------------------------------


        var chart = new Chartist.Line('#ct-line2', {
            labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
            series: [
                [12, 9, 7, 8, 5, 4, 6, 2, 3, 3, 4, 6],
                [4, 5, 3, 7, 3, 5, 5, 3, 4, 4, 5, 5],
                [5, 3, 4, 5, 6, 3, 3, 4, 5, 6, 3, 4],
                [3, 4, 5, 6, 7, 6, 4, 5, 6, 7, 6, 3]
            ]
        }, {
            low: 0,
            height: 300
        });

        // Let's put a sequence number aside so we can use it in the event callbacks
        var seq = 0,
            delays = 80,
            durations = 500;

        // Once the chart is fully created we reset the sequence
        chart.on('created', function() {
            seq = 0;
        });

        // On each drawn element by Chartist we use the Chartist.Svg API to trigger SMIL animations
        chart.on('draw', function(data) {
            seq++;

            if (data.type === 'line') {
                // If the drawn element is a line we do a simple opacity fade in. This could also be achieved using CSS3 animations.
                data.element.animate({
                    opacity: {
                        // The delay when we like to start the animation
                        begin: seq * delays + 1000,
                        // Duration of the animation
                        dur: durations,
                        // The value where the animation should start
                        from: 0,
                        // The value where it should end
                        to: 1
                    }
                });
            } else if (data.type === 'label' && data.axis === 'x') {
                data.element.animate({
                    y: {
                        begin: seq * delays,
                        dur: durations,
                        from: data.y + 100,
                        to: data.y,
                        // We can specify an easing function from Chartist.Svg.Easing
                        easing: 'easeOutQuart'
                    }
                });
            } else if (data.type === 'label' && data.axis === 'y') {
                data.element.animate({
                    x: {
                        begin: seq * delays,
                        dur: durations,
                        from: data.x - 100,
                        to: data.x,
                        easing: 'easeOutQuart'
                    }
                });
            } else if (data.type === 'point') {
                data.element.animate({
                    x1: {
                        begin: seq * delays,
                        dur: durations,
                        from: data.x - 10,
                        to: data.x,
                        easing: 'easeOutQuart'
                    },
                    x2: {
                        begin: seq * delays,
                        dur: durations,
                        from: data.x - 10,
                        to: data.x,
                        easing: 'easeOutQuart'
                    },
                    opacity: {
                        begin: seq * delays,
                        dur: durations,
                        from: 0,
                        to: 1,
                        easing: 'easeOutQuart'
                    }
                });
            } else if (data.type === 'grid') {
                // Using data.axis we get x or y which we can use to construct our animation definition objects
                var pos1Animation = {
                    begin: seq * delays,
                    dur: durations,
                    from: data[data.axis.units.pos + '1'] - 30,
                    to: data[data.axis.units.pos + '1'],
                    easing: 'easeOutQuart'
                };

                var pos2Animation = {
                    begin: seq * delays,
                    dur: durations,
                    from: data[data.axis.units.pos + '2'] - 100,
                    to: data[data.axis.units.pos + '2'],
                    easing: 'easeOutQuart'
                };

                var animations = {};
                animations[data.axis.units.pos + '1'] = pos1Animation;
                animations[data.axis.units.pos + '2'] = pos2Animation;
                animations['opacity'] = {
                    begin: seq * delays,
                    dur: durations,
                    from: 0,
                    to: 1,
                    easing: 'easeOutQuart'
                };

                data.element.animate(animations);
            }
        });

        // For the sake of the example we update the chart every time it's created with a delay of 10 seconds
        chart.on('created', function() {
            if (window.__exampleAnimateTimeout) {
                clearTimeout(window.__exampleAnimateTimeout);
                window.__exampleAnimateTimeout = null;
            }
            window.__exampleAnimateTimeout = setTimeout(chart.update.bind(chart), 12000);
        });

    }

})();
// Easypie chart Loader
// -----------------------------------

(function() {
    'use strict';

    $(initEasyPieChart);

    function initEasyPieChart() {

        if (!$.fn.easyPieChart) return;

        // Usage via data attributes
        // <div class="easypie-chart" data-easypiechart data-percent="X" data-optionName="value"></div>
        $('[data-easypiechart]').each(function() {
            var $elem = $(this);
            var options = $elem.data();
            $elem.easyPieChart(options || {});
        });

        // programmatic usage
        var pieOptions1 = {
            animate: {
                duration: 800,
                enabled: true
            },
            barColor: APP_COLORS['success'],
            trackColor: false,
            scaleColor: false,
            lineWidth: 10,
            lineCap: 'circle'
        };
        $('#easypie1').easyPieChart(pieOptions1);

        var pieOptions2 = {
            animate: {
                duration: 800,
                enabled: true
            },
            barColor: APP_COLORS['warning'],
            trackColor: false,
            scaleColor: false,
            lineWidth: 4,
            lineCap: 'circle'
        };
        $('#easypie2').easyPieChart(pieOptions2);

        var pieOptions3 = {
            animate: {
                duration: 800,
                enabled: true
            },
            barColor: APP_COLORS['danger'],
            trackColor: false,
            scaleColor: APP_COLORS['gray'],
            lineWidth: 15,
            lineCap: 'circle'
        };
        $('#easypie3').easyPieChart(pieOptions3);

        var pieOptions4 = {
            animate: {
                duration: 800,
                enabled: true
            },
            barColor: APP_COLORS['danger'],
            trackColor: APP_COLORS['yellow'],
            scaleColor: APP_COLORS['gray-dark'],
            lineWidth: 15,
            lineCap: 'circle'
        };
        $('#easypie4').easyPieChart(pieOptions4);

    }

})();
// CHART SPLINE
// -----------------------------------
(function() {
    'use strict';

    $(initFlotSpline);

    function initFlotSpline() {

        var data = [{
            "label": "Uniques",
            "color": "#768294",
            "data": [
                ["Mar", 70],
                ["Apr", 85],
                ["May", 59],
                ["Jun", 93],
                ["Jul", 66],
                ["Aug", 86],
                ["Sep", 60]
            ]
        }, {
            "label": "Recurrent",
            "color": "#1f92fe",
            "data": [
                ["Mar", 21],
                ["Apr", 12],
                ["May", 27],
                ["Jun", 24],
                ["Jul", 16],
                ["Aug", 39],
                ["Sep", 15]
            ]
        }];

        var datav2 = [{
            "label": "Hours",
            "color": "#23b7e5",
            "data": [
                ["Jan", 70],
                ["Feb", 20],
                ["Mar", 70],
                ["Apr", 85],
                ["May", 59],
                ["Jun", 93],
                ["Jul", 66],
                ["Aug", 86],
                ["Sep", 60],
                ["Oct", 60],
                ["Nov", 12],
                ["Dec", 50]
            ]
        }, {
            "label": "Commits",
            "color": "#7266ba",
            "data": [
                ["Jan", 20],
                ["Feb", 70],
                ["Mar", 30],
                ["Apr", 50],
                ["May", 85],
                ["Jun", 43],
                ["Jul", 96],
                ["Aug", 36],
                ["Sep", 80],
                ["Oct", 10],
                ["Nov", 72],
                ["Dec", 31]
            ]
        }];

        var datav3 = [{
            "label": "Home",
            "color": "#1ba3cd",
            "data": [
                ["1", 38],
                ["2", 40],
                ["3", 42],
                ["4", 48],
                ["5", 50],
                ["6", 70],
                ["7", 145],
                ["8", 70],
                ["9", 59],
                ["10", 48],
                ["11", 38],
                ["12", 29],
                ["13", 30],
                ["14", 22],
                ["15", 28]
            ]
        }, {
            "label": "Overall",
            "color": "#3a3f51",
            "data": [
                ["1", 16],
                ["2", 18],
                ["3", 17],
                ["4", 16],
                ["5", 30],
                ["6", 110],
                ["7", 19],
                ["8", 18],
                ["9", 110],
                ["10", 19],
                ["11", 16],
                ["12", 10],
                ["13", 20],
                ["14", 10],
                ["15", 20]
            ]
        }];

        var options = {
            series: {
                lines: {
                    show: false
                },
                points: {
                    show: true,
                    radius: 4
                },
                splines: {
                    show: true,
                    tension: 0.4,
                    lineWidth: 1,
                    fill: 0.5
                }
            },
            grid: {
                borderColor: '#eee',
                borderWidth: 1,
                hoverable: true,
                backgroundColor: '#fcfcfc'
            },
            tooltip: true,
            tooltipOpts: {
                content: function(label, x, y) { return x + ' : ' + y; }
            },
            xaxis: {
                tickColor: '#fcfcfc',
                mode: 'categories'
            },
            yaxis: {
                min: 0,
                max: 150, // optional: use it for a clear represetation
                tickColor: '#eee',
                //position: 'right' or 'left',
                tickFormatter: function(v) {
                    return v /* + ' visitors'*/ ;
                }
            },
            shadowSize: 0
        };

        var chart = $('.chart-spline');
        if (chart.length)
            $.plot(chart, data, options);

        var chartv2 = $('.chart-splinev2');
        if (chartv2.length)
            $.plot(chartv2, datav2, options);

        var chartv3 = $('.chart-splinev3');
        if (chartv3.length)
            $.plot(chartv3, datav3, options);

    }

})();

// CHART AREA
// -----------------------------------
(function() {
    'use strict';


    $(initFlotArea)

    function initFlotArea() {

        var data = [{
            "label": "Uniques",
            "color": "#aad874",
            "data": [
                ["Mar", 50],
                ["Apr", 84],
                ["May", 52],
                ["Jun", 88],
                ["Jul", 69],
                ["Aug", 92],
                ["Sep", 58]
            ]
        }, {
            "label": "Recurrent",
            "color": "#7dc7df",
            "data": [
                ["Mar", 13],
                ["Apr", 44],
                ["May", 44],
                ["Jun", 27],
                ["Jul", 38],
                ["Aug", 11],
                ["Sep", 39]
            ]
        }];

        var options = {
            series: {
                lines: {
                    show: true,
                    fill: 0.8
                },
                points: {
                    show: true,
                    radius: 4
                }
            },
            grid: {
                borderColor: '#eee',
                borderWidth: 1,
                hoverable: true,
                backgroundColor: '#fcfcfc'
            },
            tooltip: true,
            tooltipOpts: {
                content: function(label, x, y) { return x + ' : ' + y; }
            },
            xaxis: {
                tickColor: '#fcfcfc',
                mode: 'categories'
            },
            yaxis: {
                min: 0,
                tickColor: '#eee',
                // position: 'right' or 'left'
                tickFormatter: function(v) {
                    return v + ' visitors';
                }
            },
            shadowSize: 0
        };

        var chart = $('.chart-area');
        if (chart.length)
            $.plot(chart, data, options);

    }

})();

// CHART BAR
// -----------------------------------
(function() {
    'use strict';


    $(initFlotBar)

    function initFlotBar() {

        var data = [{
            "label": "Sales",
            "color": "#9cd159",
            "data": [
                ["Jan", 27],
                ["Feb", 82],
                ["Mar", 56],
                ["Apr", 14],
                ["May", 28],
                ["Jun", 77],
                ["Jul", 23],
                ["Aug", 49],
                ["Sep", 81],
                ["Oct", 20]
            ]
        }];

        var options = {
            series: {
                bars: {
                    align: 'center',
                    lineWidth: 0,
                    show: true,
                    barWidth: 0.6,
                    fill: 0.9
                }
            },
            grid: {
                borderColor: '#eee',
                borderWidth: 1,
                hoverable: true,
                backgroundColor: '#fcfcfc'
            },
            tooltip: true,
            tooltipOpts: {
                content: function(label, x, y) { return x + ' : ' + y; }
            },
            xaxis: {
                tickColor: '#fcfcfc',
                mode: 'categories'
            },
            yaxis: {
                // position: 'right' or 'left'
                tickColor: '#eee'
            },
            shadowSize: 0
        };

        var chart = $('.chart-bar');
        if (chart.length)
            $.plot(chart, data, options);

    }

})();


// CHART BAR STACKED
// -----------------------------------
(function() {
    'use strict';


    $(initFlotBarStacked);

    function initFlotBarStacked() {

        var data = [{
            "label": "Tweets",
            "color": "#51bff2",
            "data": [
                ["Jan", 56],
                ["Feb", 81],
                ["Mar", 97],
                ["Apr", 44],
                ["May", 24],
                ["Jun", 85],
                ["Jul", 94],
                ["Aug", 78],
                ["Sep", 52],
                ["Oct", 17],
                ["Nov", 90],
                ["Dec", 62]
            ]
        }, {
            "label": "Likes",
            "color": "#4a8ef1",
            "data": [
                ["Jan", 69],
                ["Feb", 135],
                ["Mar", 14],
                ["Apr", 100],
                ["May", 100],
                ["Jun", 62],
                ["Jul", 115],
                ["Aug", 22],
                ["Sep", 104],
                ["Oct", 132],
                ["Nov", 72],
                ["Dec", 61]
            ]
        }, {
            "label": "+1",
            "color": "#f0693a",
            "data": [
                ["Jan", 29],
                ["Feb", 36],
                ["Mar", 47],
                ["Apr", 21],
                ["May", 5],
                ["Jun", 49],
                ["Jul", 37],
                ["Aug", 44],
                ["Sep", 28],
                ["Oct", 9],
                ["Nov", 12],
                ["Dec", 35]
            ]
        }];

        var datav2 = [{
            "label": "Pending",
            "color": "#9289ca",
            "data": [
                ["Pj1", 86],
                ["Pj2", 136],
                ["Pj3", 97],
                ["Pj4", 110],
                ["Pj5", 62],
                ["Pj6", 85],
                ["Pj7", 115],
                ["Pj8", 78],
                ["Pj9", 104],
                ["Pj10", 82],
                ["Pj11", 97],
                ["Pj12", 110],
                ["Pj13", 62]
            ]
        }, {
            "label": "Assigned",
            "color": "#7266ba",
            "data": [
                ["Pj1", 49],
                ["Pj2", 81],
                ["Pj3", 47],
                ["Pj4", 44],
                ["Pj5", 100],
                ["Pj6", 49],
                ["Pj7", 94],
                ["Pj8", 44],
                ["Pj9", 52],
                ["Pj10", 17],
                ["Pj11", 47],
                ["Pj12", 44],
                ["Pj13", 100]
            ]
        }, {
            "label": "Completed",
            "color": "#564aa3",
            "data": [
                ["Pj1", 29],
                ["Pj2", 56],
                ["Pj3", 14],
                ["Pj4", 21],
                ["Pj5", 5],
                ["Pj6", 24],
                ["Pj7", 37],
                ["Pj8", 22],
                ["Pj9", 28],
                ["Pj10", 9],
                ["Pj11", 14],
                ["Pj12", 21],
                ["Pj13", 5]
            ]
        }];

        var options = {
            series: {
                stack: true,
                bars: {
                    align: 'center',
                    lineWidth: 0,
                    show: true,
                    barWidth: 0.6,
                    fill: 0.9
                }
            },
            grid: {
                borderColor: '#eee',
                borderWidth: 1,
                hoverable: true,
                backgroundColor: '#fcfcfc'
            },
            tooltip: true,
            tooltipOpts: {
                content: function(label, x, y) { return x + ' : ' + y; }
            },
            xaxis: {
                tickColor: '#fcfcfc',
                mode: 'categories'
            },
            yaxis: {
                // position: 'right' or 'left'
                tickColor: '#eee'
            },
            shadowSize: 0
        };

        var chart = $('.chart-bar-stacked');
        if (chart.length)
            $.plot(chart, data, options);

        var chartv2 = $('.chart-bar-stackedv2');
        if (chartv2.length)
            $.plot(chartv2, datav2, options);

    }

})();

// CHART DONUT
// -----------------------------------
(function() {
    'use strict';


    $(initFlotDonut);

    function initFlotDonut() {

        var data = [{
            "color": "#39C558",
            "data": 60,
            "label": "Coffee"
        }, {
            "color": "#00b4ff",
            "data": 90,
            "label": "CSS"
        }, {
            "color": "#FFBE41",
            "data": 50,
            "label": "LESS"
        }, {
            "color": "#ff3e43",
            "data": 80,
            "label": "Jade"
        }, {
            "color": "#937fc7",
            "data": 116,
            "label": "AngularJS"
        }];

        var options = {
            series: {
                pie: {
                    show: true,
                    innerRadius: 0.5 // This makes the donut shape
                }
            }
        };

        var chart = $('.chart-donut');
        if (chart.length)
            $.plot(chart, data, options);

    }

})();

// CHART LINE
// -----------------------------------
(function() {
    'use strict';


    $(initFlotLine)

    function initFlotLine() {

        var data = [{
            "label": "Complete",
            "color": "#5ab1ef",
            "data": [
                ["Jan", 188],
                ["Feb", 183],
                ["Mar", 185],
                ["Apr", 199],
                ["May", 190],
                ["Jun", 194],
                ["Jul", 194],
                ["Aug", 184],
                ["Sep", 74]
            ]
        }, {
            "label": "In Progress",
            "color": "#f5994e",
            "data": [
                ["Jan", 153],
                ["Feb", 116],
                ["Mar", 136],
                ["Apr", 119],
                ["May", 148],
                ["Jun", 133],
                ["Jul", 118],
                ["Aug", 161],
                ["Sep", 59]
            ]
        }, {
            "label": "Cancelled",
            "color": "#d87a80",
            "data": [
                ["Jan", 111],
                ["Feb", 97],
                ["Mar", 93],
                ["Apr", 110],
                ["May", 102],
                ["Jun", 93],
                ["Jul", 92],
                ["Aug", 92],
                ["Sep", 44]
            ]
        }];

        var options = {
            series: {
                lines: {
                    show: true,
                    fill: 0.01
                },
                points: {
                    show: true,
                    radius: 4
                }
            },
            grid: {
                borderColor: '#eee',
                borderWidth: 1,
                hoverable: true,
                backgroundColor: '#fcfcfc'
            },
            tooltip: true,
            tooltipOpts: {
                content: function(label, x, y) { return x + ' : ' + y; }
            },
            xaxis: {
                tickColor: '#eee',
                mode: 'categories'
            },
            yaxis: {
                // position: 'right' or 'left'
                tickColor: '#eee'
            },
            shadowSize: 0
        };

        var chart = $('.chart-line');
        if (chart.length)
            $.plot(chart, data, options);

    }

})();


// CHART PIE
// -----------------------------------
(function() {
    'use strict';


    $(initFlotPie);

    function initFlotPie() {

        var data = [{
            "label": "jQuery",
            "color": "#4acab4",
            "data": 30
        }, {
            "label": "CSS",
            "color": "#ffea88",
            "data": 40
        }, {
            "label": "LESS",
            "color": "#ff8153",
            "data": 90
        }, {
            "label": "SASS",
            "color": "#878bb6",
            "data": 75
        }, {
            "label": "Jade",
            "color": "#b2d767",
            "data": 120
        }];

        var options = {
            series: {
                pie: {
                    show: true,
                    innerRadius: 0,
                    label: {
                        show: true,
                        radius: 0.8,
                        formatter: function(label, series) {
                            return '<div class="flot-pie-label">' +
                                //label + ' : ' +
                                Math.round(series.percent) +
                                '%</div>';
                        },
                        background: {
                            opacity: 0.8,
                            color: '#222'
                        }
                    }
                }
            }
        };

        var chart = $('.chart-pie');
        if (chart.length)
            $.plot(chart, data, options);

    }

})();
// Morris
// -----------------------------------

(function() {
    'use strict';

    $(initMorris);

    function initMorris() {

        if (typeof Morris === 'undefined') return;

        var chartdata = [
            { y: "2006", a: 100, b: 90 },
            { y: "2007", a: 75, b: 65 },
            { y: "2008", a: 50, b: 40 },
            { y: "2009", a: 75, b: 65 },
            { y: "2010", a: 50, b: 40 },
            { y: "2011", a: 75, b: 65 },
            { y: "2012", a: 100, b: 90 }
        ];

        var donutdata = [
            { label: "Download Sales", value: 12 },
            { label: "In-Store Sales", value: 30 },
            { label: "Mail-Order Sales", value: 20 }
        ];

        // Line Chart
        // -----------------------------------

        new Morris.Line({
            element: 'morris-line',
            data: chartdata,
            xkey: 'y',
            ykeys: ["a", "b"],
            labels: ["Serie A", "Serie B"],
            lineColors: ["#31C0BE", "#7a92a3"],
            resize: true
        });

        // Donut Chart
        // -----------------------------------
        new Morris.Donut({
            element: 'morris-donut',
            data: donutdata,
            colors: ['#f05050', '#fad732', '#ff902b'],
            resize: true
        });

        // Bar Chart
        // -----------------------------------
        new Morris.Bar({
            element: 'morris-bar',
            data: chartdata,
            xkey: 'y',
            ykeys: ["a", "b"],
            labels: ["Series A", "Series B"],
            xLabelMargin: 2,
            barColors: ['#23b7e5', '#f05050'],
            resize: true
        });

        // Area Chart
        // -----------------------------------
        new Morris.Area({
            element: 'morris-area',
            data: chartdata,
            xkey: 'y',
            ykeys: ["a", "b"],
            labels: ["Serie A", "Serie B"],
            lineColors: ['#7266ba', '#23b7e5'],
            resize: true
        });

    }

})();
// Rickshaw
// -----------------------------------

(function() {
    'use strict';

    $(initMorris);

    function initMorris() {

        if (typeof Rickshaw === 'undefined') return;

        var seriesData = [
            [],
            [],
            []
        ];
        var random = new Rickshaw.Fixtures.RandomData(150);

        for (var i = 0; i < 150; i++) {
            random.addData(seriesData);
        }

        var series1 = [{
            color: "#c05020",
            data: seriesData[0],
            name: 'New York'
        }, {
            color: "#30c020",
            data: seriesData[1],
            name: 'London'
        }, {
            color: "#6060c0",
            data: seriesData[2],
            name: 'Tokyo'
        }];

        var graph1 = new Rickshaw.Graph({
            element: document.querySelector("#rickshaw1"),
            series: series1,
            renderer: 'area'
        });

        graph1.render();


        // Graph 2
        // -----------------------------------

        var graph2 = new Rickshaw.Graph({
            element: document.querySelector("#rickshaw2"),
            renderer: 'area',
            stroke: true,
            series: [{
                data: [{ x: 0, y: 40 }, { x: 1, y: 49 }, { x: 2, y: 38 }, { x: 3, y: 30 }, { x: 4, y: 32 }],
                color: '#f05050'
            }, {
                data: [{ x: 0, y: 40 }, { x: 1, y: 49 }, { x: 2, y: 38 }, { x: 3, y: 30 }, { x: 4, y: 32 }],
                color: '#fad732'
            }]
        });

        graph2.render();

        // Graph 3
        // -----------------------------------


        var graph3 = new Rickshaw.Graph({
            element: document.querySelector("#rickshaw3"),
            renderer: 'line',
            series: [{
                data: [{ x: 0, y: 40 }, { x: 1, y: 49 }, { x: 2, y: 38 }, { x: 3, y: 30 }, { x: 4, y: 32 }],
                color: '#7266ba'
            }, {
                data: [{ x: 0, y: 20 }, { x: 1, y: 24 }, { x: 2, y: 19 }, { x: 3, y: 15 }, { x: 4, y: 16 }],
                color: '#23b7e5'
            }]
        });
        graph3.render();


        // Graph 4
        // -----------------------------------


        var graph4 = new Rickshaw.Graph({
            element: document.querySelector("#rickshaw4"),
            renderer: 'bar',
            series: [{
                data: [{ x: 0, y: 40 }, { x: 1, y: 49 }, { x: 2, y: 38 }, { x: 3, y: 30 }, { x: 4, y: 32 }],
                color: '#fad732'
            }, {
                data: [{ x: 0, y: 20 }, { x: 1, y: 24 }, { x: 2, y: 19 }, { x: 3, y: 15 }, { x: 4, y: 16 }],
                color: '#ff902b'

            }]
        });
        graph4.render();

    }

})();
// SPARKLINE
// -----------------------------------

(function() {
    'use strict';

    $(initSparkline);

    function initSparkline() {

        $('[data-sparkline]').each(initSparkLine);

        function initSparkLine() {
            var $element = $(this),
                options = $element.data(),
                values = options.values && options.values.split(',');

            options.type = options.type || 'bar'; // default chart is bar
            options.disableHiddenCheck = true;

            $element.sparkline(values, options);

            if (options.resize) {
                $(window).resize(function() {
                    $element.sparkline(values, options);
                });
            }
        }
    }

})();
// Start Bootstrap JS
// -----------------------------------

(function() {
    'use strict';

    $(initBootstrap);

    function initBootstrap() {

        // necessary check at least til BS doesn't require jQuery
        if (!$.fn || !$.fn.tooltip || !$.fn.popover) return;

        // POPOVER
        // -----------------------------------

        $('[data-toggle="popover"]').popover();

        // TOOLTIP
        // -----------------------------------

        $('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });

        // DROPDOWN INPUTS
        // -----------------------------------
        $('.dropdown input').on('click focus', function(event) {
            event.stopPropagation();
        });

    }

})();
// Module: card-tools
// -----------------------------------

(function() {
    'use strict';

    $(initCardDismiss);
    $(initCardCollapse);
    $(initCardRefresh);


    /**
     * Helper function to find the closest
     * ascending .card element
     */
    function getCardParent(item) {
        var el = item.parentElement;
        while (el && !el.classList.contains('card'))
            el = el.parentElement
        return el
    }
    /**
     * Helper to trigger custom event
     */
    function triggerEvent(type, item, data) {
        var ev;
        if (typeof CustomEvent === 'function') {
            ev = new CustomEvent(type, { detail: data });
        } else {
            ev = document.createEvent('CustomEvent');
            ev.initCustomEvent(type, true, false, data);
        }
        item.dispatchEvent(ev);
    }

    /**
     * Dismiss cards
     * [data-tool="card-dismiss"]
     */
    function initCardDismiss() {
        var cardtoolSelector = '[data-tool="card-dismiss"]'

        var cardList = [].slice.call(document.querySelectorAll(cardtoolSelector))

        cardList.forEach(function(item) {
            new CardDismiss(item);
        })

        function CardDismiss(item) {
            var EVENT_REMOVE = 'card.remove';
            var EVENT_REMOVED = 'card.removed';

            this.item = item;
            this.cardParent = getCardParent(this.item);
            this.removing = false; // prevents double execution

            this.clickHandler = function(e) {
                if (this.removing) return;
                this.removing = true;
                // pass callbacks via event.detail to confirm/cancel the removal
                triggerEvent(EVENT_REMOVE, this.cardParent, {
                    confirm: this.confirm.bind(this),
                    cancel: this.cancel.bind(this)
                });
            }
            this.confirm = function() {
                this.animate(this.cardParent, function() {
                    triggerEvent(EVENT_REMOVED, this.cardParent);
                    this.remove(this.cardParent);
                })
            }
            this.cancel = function() {
                this.removing = false;
            }
            this.animate = function(item, cb) {
                if ('onanimationend' in window) { // animation supported
                    item.addEventListener('animationend', cb.bind(this))
                    item.className += ' animated bounceOut'; // requires animate.css
                } else cb.call(this) // no animation, just remove
            }
            this.remove = function(item) {
                item.parentNode.removeChild(item);
            }
            // attach listener
            item.addEventListener('click', this.clickHandler.bind(this), false)
        }
    }


    /**
     * Collapsed cards
     * [data-tool="card-collapse"]
     * [data-start-collapsed]
     */
    function initCardCollapse() {
        var cardtoolSelector = '[data-tool="card-collapse"]';
        var cardList = [].slice.call(document.querySelectorAll(cardtoolSelector))

        cardList.forEach(function(item) {
            var initialState = item.hasAttribute('data-start-collapsed')
            new CardCollapse(item, initialState);
        })

        function CardCollapse(item, startCollapsed) {
            var EVENT_SHOW = 'card.collapse.show';
            var EVENT_HIDE = 'card.collapse.hide';

            this.state = true; // true -> show / false -> hide
            this.item = item;
            this.cardParent = getCardParent(this.item);
            this.wrapper = this.cardParent.querySelector('.card-wrapper');

            this.toggleCollapse = function(action) {
                triggerEvent(action ? EVENT_SHOW : EVENT_HIDE, this.cardParent)
                this.wrapper.style.maxHeight = (action ? this.wrapper.scrollHeight : 0) + 'px'
                this.state = action;
                this.updateIcon(action)
            }
            this.updateIcon = function(action) {
                this.item.firstElementChild.className = action ? 'fa fa-minus' : 'fa fa-plus'
            }
            this.clickHandler = function() {
                this.toggleCollapse(!this.state);
            }
            this.initStyles = function() {
                this.wrapper.style.maxHeight = this.wrapper.scrollHeight + 'px';
                this.wrapper.style.transition = 'max-height 0.5s';
                this.wrapper.style.overflow = 'hidden';
            }

            // prepare styles for collapse animation
            this.initStyles()
            // set initial state if provided
            if (startCollapsed) {
                this.toggleCollapse(false)
            }
            // attach listener
            this.item.addEventListener('click', this.clickHandler.bind(this), false)

        }
    }


    /**
     * Refresh cards
     * [data-tool="card-refresh"]
     * [data-spinner="standard"]
     */
    function initCardRefresh() {

        var cardtoolSelector = '[data-tool="card-refresh"]';
        var cardList = [].slice.call(document.querySelectorAll(cardtoolSelector))

        cardList.forEach(function(item) {
            new CardRefresh(item);
        })

        function CardRefresh(item) {
            var EVENT_REFRESH = 'card.refresh';
            var WHIRL_CLASS = 'whirl';
            var DEFAULT_SPINNER = 'standard'

            this.item = item;
            this.cardParent = getCardParent(this.item)
            this.spinner = ((this.item.dataset || {}).spinner || DEFAULT_SPINNER).split(' '); // support space separated classes

            this.refresh = function(e) {
                var card = this.cardParent;
                // start showing the spinner
                this.showSpinner(card, this.spinner)
                // attach as public method
                card.removeSpinner = this.removeSpinner.bind(this);
                // Trigger the event and send the card
                triggerEvent(EVENT_REFRESH, card, { card: card });
            }
            this.showSpinner = function(card, spinner) {
                card.classList.add(WHIRL_CLASS);
                spinner.forEach(function(s) { card.classList.add(s) })
            }
            this.removeSpinner = function() {
                this.cardParent.classList.remove(WHIRL_CLASS);
            }

            // attach listener
            this.item.addEventListener('click', this.refresh.bind(this), false)

        }
    }

})();
// GLOBAL CONSTANTS
// -----------------------------------

(function() {

    window.APP_COLORS = {
        'primary':                '#5d9cec',
        'success':                '#27c24c',
        'info':                   '#23b7e5',
        'warning':                '#ff902b',
        'danger':                 '#f05050',
        'inverse':                '#131e26',
        'green':                  '#37bc9b',
        'pink':                   '#f532e5',
        'purple':                 '#7266ba',
        'dark':                   '#3a3f51',
        'yellow':                 '#fad732',
        'gray-darker':            '#232735',
        'gray-dark':              '#3a3f51',
        'gray':                   '#dde6e9',
        'gray-light':             '#e4eaec',
        'gray-lighter':           '#edf1f2'
    };

    window.APP_MEDIAQUERY = {
        'desktopLG':             1200,
        'desktop':                992,
        'tablet':                 768,
        'mobile':                 480
    };

})();
// FULLSCREEN
// -----------------------------------

(function() {
    'use strict';

    $(initScreenFull);

    function initScreenFull() {
        if (typeof screenfull === 'undefined') return;

        var $doc = $(document);
        var $fsToggler = $('[data-toggle-fullscreen]');

        // Not supported under IE
        var ua = window.navigator.userAgent;
        if (ua.indexOf("MSIE ") > 0 || !!ua.match(/Trident.*rv\:11\./)) {
            $fsToggler.addClass('d-none'); // hide element
            return; // and abort
        }

        $fsToggler.on('click', function(e) {
            e.preventDefault();

            if (screenfull.enabled) {

                screenfull.toggle();

                // Switch icon indicator
                toggleFSIcon($fsToggler);

            } else {
                console.log('Fullscreen not enabled');
            }
        });

        if (screenfull.raw && screenfull.raw.fullscreenchange)
            $doc.on(screenfull.raw.fullscreenchange, function() {
                toggleFSIcon($fsToggler);
            });

        function toggleFSIcon($element) {
            if (screenfull.isFullscreen)
                $element.children('em').removeClass('fa-expand').addClass('fa-compress');
            else
                $element.children('em').removeClass('fa-compress').addClass('fa-expand');
        }

    }

})();
// LOAD CUSTOM CSS
// -----------------------------------

(function() {
    'use strict';

    $(initLoadCSS);

    function initLoadCSS() {

        $('[data-load-css]').on('click', function(e) {

            var element = $(this);

            if (element.is('a'))
                e.preventDefault();

            var uri = element.data('loadCss'),
                link;

            if (uri) {
                link = createLink(uri);
                if (!link) {
                    $.error('Error creating stylesheet link element.');
                }
            } else {
                $.error('No stylesheet location defined.');
            }

        });
    }

    function createLink(uri) {
        var linkId = 'autoloaded-stylesheet',
            oldLink = $('#' + linkId).attr('id', linkId + '-old');

        $('head').append($('<link/>').attr({
            'id': linkId,
            'rel': 'stylesheet',
            'href': uri
        }));

        if (oldLink.length) {
            oldLink.remove();
        }

        return $('#' + linkId);
    }

})();
// TRANSLATION
// -----------------------------------

(function() {
    'use strict';

    $(initTranslation);
    
    var ROOT_URL = window.location.protocol + "//" + window.location.host;
    //var pathPrefix = ROOT_URL + '/i18n'; // folder of json files
    var pathPrefix = 'https://fundperdromartinez.azurewebsites.net/i18n'; // folder of json files
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
// NAVBAR SEARCH
// -----------------------------------

(function() {
    'use strict';

    $(initNavbarSearch);

    function initNavbarSearch() {

        var navSearch = new navbarSearchInput();

        // Open search input
        var $searchOpen = $('[data-search-open]');

        $searchOpen
            .on('click', function(e) { e.stopPropagation(); })
            .on('click', navSearch.toggle);

        // Close search input
        var $searchDismiss = $('[data-search-dismiss]');
        var inputSelector = '.navbar-form input[type="text"]';

        $(inputSelector)
            .on('click', function(e) { e.stopPropagation(); })
            .on('keyup', function(e) {
                if (e.keyCode == 27) // ESC
                    navSearch.dismiss();
            });

        // click anywhere closes the search
        $(document).on('click', navSearch.dismiss);
        // dismissable options
        $searchDismiss
            .on('click', function(e) { e.stopPropagation(); })
            .on('click', navSearch.dismiss);

    }

    var navbarSearchInput = function() {
        var navbarFormSelector = 'form.navbar-form';
        return {
            toggle: function() {

                var navbarForm = $(navbarFormSelector);

                navbarForm.toggleClass('open');

                var isOpen = navbarForm.hasClass('open');

                navbarForm.find('input')[isOpen ? 'focus' : 'blur']();

            },

            dismiss: function() {
                $(navbarFormSelector)
                    .removeClass('open') // Close control
                    .find('input[type="text"]').blur() // remove focus
                // .val('')                    // Empty input
                ;
            }
        };

    }

})();
// NOW TIMER
// -----------------------------------

(function() {
    'use strict';

    $(initNowTimer);

    function initNowTimer() {

        if (typeof moment === 'undefined') return;

        $('[data-now]').each(function() {
            var element = $(this),
                format = element.data('format');

            function updateTime() {
                var dt = moment(new Date()).format(format);
                element.text(dt);
            }

            updateTime();
            setInterval(updateTime, 1000);

        });
    }

})();
// Toggle RTL mode for demo
// -----------------------------------


(function() {
    'use strict';

    $(initRTL);

    function initRTL() {
        var maincss = $('#maincss');
        var bscss = $('#bscss');
        $('#chk-rtl').on('change', function() {
            // app rtl check
            maincss.attr('href', this.checked ? '/css/app-rtl.css' : '/css/app.css');
            // bootstrap rtl check
            bscss.attr('href', this.checked ? '/css/bootstrap-rtl.css' : '/css/bootstrap.css');
        });
    }

})();
// SIDEBAR
// -----------------------------------


(function() {
    'use strict';

    $(initSidebar);

    var $html;
    var $body;
    var $sidebar;

    function initSidebar() {

        $html = $('html');
        $body = $('body');
        $sidebar = $('.sidebar');

        // AUTOCOLLAPSE ITEMS
        // -----------------------------------

        var sidebarCollapse = $sidebar.find('.collapse');
        sidebarCollapse.on('show.bs.collapse', function(event) {

            event.stopPropagation();
            if ($(this).parents('.collapse').length === 0)
                sidebarCollapse.filter('.show').collapse('hide');

        });

        // SIDEBAR ACTIVE STATE
        // -----------------------------------

        // Find current active item
        var currentItem = $('.sidebar .active').parents('li');

        // hover mode don't try to expand active collapse
        if (!useAsideHover())
            currentItem
            .addClass('active') // activate the parent
            .children('.collapse') // find the collapse
            .collapse('show'); // and show it

        // remove this if you use only collapsible sidebar items
        $sidebar.find('li > a + ul').on('show.bs.collapse', function(e) {
            if (useAsideHover()) e.preventDefault();
        });

        // SIDEBAR COLLAPSED ITEM HANDLER
        // -----------------------------------


        var eventName = isTouch() ? 'click' : 'mouseenter';
        var subNav = $();
        $sidebar.find('.sidebar-nav > li').on(eventName, function(e) {

            if (isSidebarCollapsed() || useAsideHover()) {

                subNav.trigger('mouseleave');
                subNav = toggleMenuItem($(this));

                // Used to detect click and touch events outside the sidebar
                sidebarAddBackdrop();
            }

        });

        var sidebarAnyclickClose = $sidebar.data('sidebarAnyclickClose');

        // Allows to close
        if (typeof sidebarAnyclickClose !== 'undefined') {

            $('.wrapper').on('click.sidebar', function(e) {
                // don't check if sidebar not visible
                if (!$body.hasClass('aside-toggled')) return;

                var $target = $(e.target);
                if (!$target.parents('.aside-container').length && // if not child of sidebar
                    !$target.is('#user-block-toggle') && // user block toggle anchor
                    !$target.parent().is('#user-block-toggle') // user block toggle icon
                ) {
                    $body.removeClass('aside-toggled');
                }

            });
        }
    }

    function sidebarAddBackdrop() {
        var $backdrop = $('<div/>', { 'class': 'sidebar-backdrop' });
        $backdrop.insertAfter('.aside-container').on("click mouseenter", function() {
            removeFloatingNav();
        });
    }

    // Open the collapse sidebar submenu items when on touch devices
    // - desktop only opens on hover
    function toggleTouchItem($element) {
        $element
            .siblings('li')
            .removeClass('open')
        $element
            .toggleClass('open');
    }

    // Handles hover to open items under collapsed menu
    // -----------------------------------
    function toggleMenuItem($listItem) {

        removeFloatingNav();

        var ul = $listItem.children('ul');

        if (!ul.length) return $();
        if ($listItem.hasClass('open')) {
            toggleTouchItem($listItem);
            return $();
        }

        var $aside = $('.aside-container');
        var $asideInner = $('.aside-inner'); // for top offset calculation
        // float aside uses extra padding on aside
        var mar = parseInt($asideInner.css('padding-top'), 0) + parseInt($aside.css('padding-top'), 0);

        var subNav = ul.clone().appendTo($aside);

        toggleTouchItem($listItem);

        var itemTop = ($listItem.position().top + mar) - $sidebar.scrollTop();
        var vwHeight = document.body.clientHeight;

        subNav
            .addClass('nav-floating')
            .css({
                position: isFixed() ? 'fixed' : 'absolute',
                top: itemTop,
                bottom: (subNav.outerHeight(true) + itemTop > vwHeight) ? 0 : 'auto'
            });

        subNav.on('mouseleave', function() {
            toggleTouchItem($listItem);
            subNav.remove();
        });

        return subNav;
    }

    function removeFloatingNav() {
        $('.sidebar-subnav.nav-floating').remove();
        $('.sidebar-backdrop').remove();
        $('.sidebar li.open').removeClass('open');
    }

    function isTouch() {
        return $html.hasClass('touch');
    }

    function isSidebarCollapsed() {
        return $body.hasClass('aside-collapsed') || $body.hasClass('aside-collapsed-text');
    }

    function isSidebarToggled() {
        return $body.hasClass('aside-toggled');
    }

    function isMobile() {
        return document.body.clientWidth < APP_MEDIAQUERY.tablet;
    }

    function isFixed() {
        return $body.hasClass('layout-fixed');
    }

    function useAsideHover() {
        return $body.hasClass('aside-hover');
    }

})();
// SLIMSCROLL
// -----------------------------------

(function() {
    'use strict';

    $(initSlimsSroll);

    function initSlimsSroll() {

        if (!$.fn || !$.fn.slimScroll) return;

        $('[data-scrollable]').each(function() {

            var element = $(this),
                defaultHeight = 250;

            element.slimScroll({
                height: (element.data('height') || defaultHeight)
            });

        });
    }

})();
// Table Check All
// -----------------------------------

(function() {
    'use strict';

    $(initTableCheckAll);

    function initTableCheckAll() {

        $('[data-check-all]').on('change', function() {
            var $this = $(this),
                index = $this.index() + 1,
                checkbox = $this.find('input[type="checkbox"]'),
                table = $this.parents('table');
            // Make sure to affect only the correct checkbox column
            table.find('tbody > tr > td:nth-child(' + index + ') input[type="checkbox"]')
                .prop('checked', checkbox[0].checked);

        });

    }

})();
// TOGGLE STATE
// -----------------------------------

(function() {
    'use strict';

    $(initToggleState);

    function initToggleState() {

        var $body = $('body');
        var toggle = new StateToggler();

        $('[data-toggle-state]')
            .on('click', function(e) {
                // e.preventDefault();
                e.stopPropagation();
                var element = $(this),
                    classname = element.data('toggleState'),
                    target = element.data('target'),
                    noPersist = (element.attr('data-no-persist') !== undefined);

                // Specify a target selector to toggle classname
                // use body by default
                var $target = target ? $(target) : $body;

                if (classname) {
                    if ($target.hasClass(classname)) {
                        $target.removeClass(classname);
                        if (!noPersist)
                            toggle.removeState(classname);
                    } else {
                        $target.addClass(classname);
                        if (!noPersist)
                            toggle.addState(classname);
                    }

                }

                // some elements may need this when toggled class change the content size
                if (typeof(Event) === 'function') { // modern browsers
                    window.dispatchEvent(new Event('resize'));
                } else { // old browsers and IE
                    var resizeEvent = window.document.createEvent('UIEvents');
                    resizeEvent.initUIEvent('resize', true, false, window, 0);
                    window.dispatchEvent(resizeEvent);
                }
            });

    }

    // Handle states to/from localstorage
    var StateToggler = function() {

        var STORAGE_KEY_NAME = 'jq-toggleState';

        /** Add a state to the browser storage to be restored later */
        this.addState = function(classname) {
            var data = Storages.localStorage.get(STORAGE_KEY_NAME);
            if (data instanceof Array) data.push(classname);
            else data = [classname];
            Storages.localStorage.set(STORAGE_KEY_NAME, data);
        };
        /** Remove a state from the browser storage */
        this.removeState = function(classname) {
            var data = Storages.localStorage.get(STORAGE_KEY_NAME);
            if (data) {
                var index = data.indexOf(classname);
                if (index !== -1) data.splice(index, 1);
                Storages.localStorage.set(STORAGE_KEY_NAME, data);
            }
        };
        /** Load the state string and restore the classlist */
        this.restoreState = function($elem) {
            var data = Storages.localStorage.get(STORAGE_KEY_NAME);
            if (data instanceof Array)
                $elem.addClass(data.join(' '));
        };
    };

    window.StateToggler = StateToggler;

})();
/**=========================================================
 * Module: trigger-resize.js
 * Triggers a window resize event from any element
 =========================================================*/

(function() {
    'use strict';

    $(initTriggerResize);

    function initTriggerResize() {
        var element = $('[data-trigger-resize]');
        var value = element.data('triggerResize')
        element.on('click', function() {
            setTimeout(function() {
                // all IE friendly dispatchEvent
                var evt = document.createEvent('UIEvents');
                evt.initUIEvent('resize', true, false, window, 0);
                window.dispatchEvent(evt);
                // modern dispatchEvent way
                // window.dispatchEvent(new Event('resize'));
            }, value || 300);
        });
    }

})();
// Demo Cards
// -----------------------------------

(function() {
    'use strict';

    $(initCardDemo);

    function initCardDemo() {

        /**
         * This functions show a demonstration of how to use
         * the card tools system via custom event.
         */
        var cardList = [].slice.call(document.querySelectorAll('.card.card-demo'));
        cardList.forEach(function(item) {

            item.addEventListener('card.refresh', function(event) {
                // get the card element that is refreshing
                var card = event.detail.card;
                // perform any action here, when it is done,
                // remove the spinner calling "removeSpinner"
                // setTimeout used to simulate async operation
                setTimeout(card.removeSpinner, 3000);
            })
            item.addEventListener('card.collapse.hide', function() {
                console.log('Card Collapse Hide');
            })
            item.addEventListener('card.collapse.show', function() {
                console.log('Card Collapse Show');
            })
            item.addEventListener('card.remove', function(event) {
                var confirm = event.detail.confirm;
                var cancel = event.detail.cancel;
                // perform any action  here
                console.log('Removing Card');
                // Call confirm() to continue removing card
                // otherwise call cancel()
                confirm();
            })
            item.addEventListener('card.removed', function(event) {
                console.log('Removed Card');
            });

        })

    }

})();
// Nestable demo
// -----------------------------------

(function() {
    'use strict';

    $(initNestable);

    function initNestable() {

        if (!$.fn.nestable) return;

        var updateOutput = function(e) {
            var list = e.length ? e : $(e.target),
                output = list.data('output');
            if (window.JSON) {
                output.val(window.JSON.stringify(list.nestable('serialize'))); //, null, 2));
            } else {
                output.val('JSON browser support required for this demo.');
            }
        };

        // activate Nestable for list 1
        $('#nestable').nestable({
                group: 1
            })
            .on('change', updateOutput);

        // activate Nestable for list 2
        $('#nestable2').nestable({
                group: 1
            })
            .on('change', updateOutput);

        // output initial serialised data
        updateOutput($('#nestable').data('output', $('#nestable-output')));
        updateOutput($('#nestable2').data('output', $('#nestable2-output')));

        $('.js-nestable-action').on('click', function(e) {
            var target = $(e.target),
                action = target.data('action');
            if (action === 'expand-all') {
                $('.dd').nestable('expandAll');
            }
            if (action === 'collapse-all') {
                $('.dd').nestable('collapseAll');
            }
        });

    }

})();
/**=========================================================
 * Module: notify.js
 * Create toggleable notifications that fade out automatically.
 * Based on Notify addon from UIKit (http://getuikit.com/docs/addons_notify.html)
 * [data-toggle="notify"]
 * [data-options="options in json format" ]
 =========================================================*/

(function() {
    'use strict';

    $(initNotify);

    function initNotify() {

        var Selector = '[data-notify]',
            autoloadSelector = '[data-onload]',
            doc = $(document);

        $(Selector).each(function() {

            var $this = $(this),
                onload = $this.data('onload');

            if (onload !== undefined) {
                setTimeout(function() {
                    notifyNow($this);
                }, 800);
            }

            $this.on('click', function(e) {
                e.preventDefault();
                notifyNow($this);
            });

        });

    }

    function notifyNow($element) {
        var message = $element.data('message'),
            options = $element.data('options');

        if (!message)
            $.error('Notify: No message specified');

        $.notify(message, options || {});
    }


})();


/**
 * Notify Addon definition as jQuery plugin
 * Adapted version to work with Bootstrap classes
 * More information http://getuikit.com/docs/addons_notify.html
 */

(function() {

    var containers = {},
        messages = {},

        notify = function(options) {

            if ($.type(options) == 'string') {
                options = { message: options };
            }

            if (arguments[1]) {
                options = $.extend(options, $.type(arguments[1]) == 'string' ? { status: arguments[1] } : arguments[1]);
            }

            return (new Message(options)).show();
        },
        closeAll = function(group, instantly) {
            if (group) {
                for (var id in messages) { if (group === messages[id].group) messages[id].close(instantly); }
            } else {
                for (var id in messages) { messages[id].close(instantly); }
            }
        };

    var Message = function(options) {

        var $this = this;

        this.options = $.extend({}, Message.defaults, options);

        this.uuid = "ID" + (new Date().getTime()) + "RAND" + (Math.ceil(Math.random() * 100000));
        this.element = $([
            // alert-dismissable enables bs close icon
            '<div class="uk-notify-message alert-dismissable">',
            '<a class="close">&times;</a>',
            '<div>' + this.options.message + '</div>',
            '</div>'

        ].join('')).data("notifyMessage", this);

        // status
        if (this.options.status) {
            this.element.addClass('alert alert-' + this.options.status);
            this.currentstatus = this.options.status;
        }

        this.group = this.options.group;

        messages[this.uuid] = this;

        if (!containers[this.options.pos]) {
            containers[this.options.pos] = $('<div class="uk-notify uk-notify-' + this.options.pos + '"></div>').appendTo('body').on("click", ".uk-notify-message", function() {
                $(this).data("notifyMessage").close();
            });
        }
    };


    $.extend(Message.prototype, {

        uuid: false,
        element: false,
        timout: false,
        currentstatus: "",
        group: false,

        show: function() {

            if (this.element.is(":visible")) return;

            var $this = this;

            containers[this.options.pos].show().prepend(this.element);

            var marginbottom = parseInt(this.element.css("margin-bottom"), 10);

            this.element.css({ "opacity": 0, "margin-top": -1 * this.element.outerHeight(), "margin-bottom": 0 }).animate({ "opacity": 1, "margin-top": 0, "margin-bottom": marginbottom }, function() {

                if ($this.options.timeout) {

                    var closefn = function() { $this.close(); };

                    $this.timeout = setTimeout(closefn, $this.options.timeout);

                    $this.element.hover(
                        function() { clearTimeout($this.timeout); },
                        function() { $this.timeout = setTimeout(closefn, $this.options.timeout); }
                    );
                }

            });

            return this;
        },

        close: function(instantly) {

            var $this = this,
                finalize = function() {
                    $this.element.remove();

                    if (!containers[$this.options.pos].children().length) {
                        containers[$this.options.pos].hide();
                    }

                    delete messages[$this.uuid];
                };

            if (this.timeout) clearTimeout(this.timeout);

            if (instantly) {
                finalize();
            } else {
                this.element.animate({ "opacity": 0, "margin-top": -1 * this.element.outerHeight(), "margin-bottom": 0 }, function() {
                    finalize();
                });
            }
        },

        content: function(html) {

            var container = this.element.find(">div");

            if (!html) {
                return container.html();
            }

            container.html(html);

            return this;
        },

        status: function(status) {

            if (!status) {
                return this.currentstatus;
            }

            this.element.removeClass('alert alert-' + this.currentstatus).addClass('alert alert-' + status);

            this.currentstatus = status;

            return this;
        }
    });

    Message.defaults = {
        message: "",
        status: "normal",
        timeout: 5000,
        group: null,
        pos: 'top-center'
    };


    $["notify"] = notify;
    $["notify"].message = Message;
    $["notify"].closeAll = closeAll;

    return notify;

}());
/**=========================================================
 * Module: portlet.js
 * Drag and drop any card to change its position
 * The Selector should could be applied to any object that contains
 * card, so .col-* element are ideal.
 =========================================================*/

(function() {
    'use strict';

    var STORAGE_KEY_NAME = 'jq-portletState';

    $(initPortlets);

    function initPortlets() {

        // Component is NOT optional
        if (!$.fn.sortable) return;

        var Selector = '[data-toggle="portlet"]';

        $(Selector).sortable({
            connectWith:          Selector,
            items:                'div.card',
            handle:               '.portlet-handler',
            opacity:              0.7,
            placeholder:          'portlet box-placeholder',
            cancel:               '.portlet-cancel',
            forcePlaceholderSize: true,
            iframeFix:            false,
            tolerance:            'pointer',
            helper:               'original',
            revert:               200,
            forceHelperSize:      true,
            update:               savePortletOrder,
            create:               loadPortletOrder
        })
        // optionally disables mouse selection
        //.disableSelection()
        ;

    }

    function savePortletOrder(event, ui) {

        var data = Storages.localStorage.get(STORAGE_KEY_NAME);

        if (!data) { data = {}; }

        data[this.id] = $(this).sortable('toArray');

        if (data) {
            Storages.localStorage.set(STORAGE_KEY_NAME, data);
        }

    }

    function loadPortletOrder() {

        var data = Storages.localStorage.get(STORAGE_KEY_NAME);

        if (data) {

            var porletId = this.id,
                cards = data[porletId];

            if (cards) {
                var portlet = $('#' + porletId);

                $.each(cards, function(index, value) {
                    $('#' + value).appendTo(portlet);
                });
            }

        }

    }

    // Reset porlet save state
    window.resetPorlets = function(e) {
        Storages.localStorage.remove(STORAGE_KEY_NAME);
        // reload the page
        window.location.reload();
    }

})();
// HTML5 Sortable demo
// -----------------------------------

(function() {
    'use strict';

    $(initSortable);

    function initSortable() {

        if (typeof sortable === 'undefined') return;

        sortable('.sortable', {
            forcePlaceholderSize: true,
            placeholder: '<div class="box-placeholder p0 m0"><div></div></div>'
        });

    }

})();
// Sweet Alert
// -----------------------------------

(function() {
    'use strict';

    $(initSweetAlert);

    function initSweetAlert() {

        $('#swal-demo1').on('click', function(e) {
            e.preventDefault();
            swal("Here's a message!")
        });

        $('#swal-demo2').on('click', function(e) {
            e.preventDefault();
            swal("Here's a message!", "It's pretty, isn't it?")
        });

        $('#swal-demo3').on('click', function(e) {
            e.preventDefault();
            swal("Good job!", "You clicked the button!", "success")
        });

        $('#swal-demo4').on('click', function(e) {
            e.preventDefault();
            swal({
                title: 'Are you sure?',
                text: 'Your will not be able to recover this imaginary file!',
                icon: 'warning',
                buttons: {
                    cancel: true,
                    confirm: {
                        text: 'Yes, delete it!',
                        value: true,
                        visible: true,
                        className: "bg-danger",
                        closeModal: true
                    }
                }
            }).then(function() {
                swal('Booyah!');
            });

        });

        $('#swal-demo5').on('click', function(e) {
            e.preventDefault();
            swal({
                title: 'Are you sure?',
                text: 'Your will not be able to recover this imaginary file!',
                icon: 'warning',
                buttons: {
                    cancel: {
                        text: 'No, cancel plx!',
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: false
                    },
                    confirm: {
                        text: 'Yes, delete it!',
                        value: true,
                        visible: true,
                        className: "bg-danger",
                        closeModal: false
                    }
                }
            }).then(function(isConfirm) {
                if (isConfirm) {
                    swal('Deleted!', 'Your imaginary file has been deleted.', 'success');
                } else {
                    swal('Cancelled', 'Your imaginary file is safe :)', 'error');
                }
            });

        });

    }

})();
// Color picker
// -----------------------------------

(function() {
    'use strict';

    $(initColorPicker);

    function initColorPicker() {

        if (!$.fn.colorpicker) return;

        $('.demo-colorpicker').colorpicker();

        $('#demo_selectors').colorpicker({
            colorSelectors: {
                'default': '#777777',
                'primary': APP_COLORS['primary'],
                'success': APP_COLORS['success'],
                'info': APP_COLORS['info'],
                'warning': APP_COLORS['warning'],
                'danger': APP_COLORS['danger']
            }
        });

    }

})();
// Forms Demo
// -----------------------------------


(function() {
    'use strict';

    $(initFormsDemo);

    function initFormsDemo() {

        if (!$.fn.slider) return;
        if (!$.fn.chosen) return;
        if (!$.fn.inputmask) return;
        if (!$.fn.filestyle) return;
        if (!$.fn.wysiwyg) return;
        if (!$.fn.datepicker) return;

        // BOOTSTRAP SLIDER CTRL
        // -----------------------------------

        $('[data-ui-slider]').slider();

        // CHOSEN
        // -----------------------------------

        $('.chosen-select').chosen();

        // MASKED
        // -----------------------------------

        $('[data-masked]').inputmask();

        // FILESTYLE
        // -----------------------------------

        $('.filestyle').filestyle();

        // WYSIWYG
        // -----------------------------------

        $('.wysiwyg').wysiwyg();


        // DATETIMEPICKER
        // -----------------------------------

        $('#datetimepicker1').datepicker({
            orientation: 'bottom',
            icons: {
                time: 'fa fa-clock-o',
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-crosshairs',
                clear: 'fa fa-trash'
            }
        });
        // only time
        $('#datetimepicker2').datepicker({
            format: 'mm-dd-yyyy'
        });

    }

})();
/**=========================================================
 * Module: Image Cropper
 =========================================================*/

(function() {
    'use strict';

    $(initImageCropper);

    function initImageCropper() {

        if (!$.fn.cropper) return;

        var $image = $('.img-container > img'),
            $dataX = $('#dataX'),
            $dataY = $('#dataY'),
            $dataHeight = $('#dataHeight'),
            $dataWidth = $('#dataWidth'),
            $dataRotate = $('#dataRotate'),
            options = {
                // data: {
                //   x: 420,
                //   y: 60,
                //   width: 640,
                //   height: 360
                // },
                // strict: false,
                // responsive: false,
                // checkImageOrigin: false

                // modal: false,
                // guides: false,
                // highlight: false,
                // background: false,

                // autoCrop: false,
                // autoCropArea: 0.5,
                // dragCrop: false,
                // movable: false,
                // rotatable: false,
                // zoomable: false,
                // touchDragZoom: false,
                // mouseWheelZoom: false,
                // cropBoxMovable: false,
                // cropBoxResizable: false,
                // doubleClickToggle: false,

                // minCanvasWidth: 320,
                // minCanvasHeight: 180,
                // minCropBoxWidth: 160,
                // minCropBoxHeight: 90,
                // minContainerWidth: 320,
                // minContainerHeight: 180,

                // build: null,
                // built: null,
                // dragstart: null,
                // dragmove: null,
                // dragend: null,
                // zoomin: null,
                // zoomout: null,

                aspectRatio: 16 / 9,
                preview: '.img-preview',
                crop: function(data) {
                    $dataX.val(Math.round(data.x));
                    $dataY.val(Math.round(data.y));
                    $dataHeight.val(Math.round(data.height));
                    $dataWidth.val(Math.round(data.width));
                    $dataRotate.val(Math.round(data.rotate));
                }
            };

        $image.on({
            'build.cropper': function(e) {
                console.log(e.type);
            },
            'built.cropper': function(e) {
                console.log(e.type);
            },
            'dragstart.cropper': function(e) {
                console.log(e.type, e.dragType);
            },
            'dragmove.cropper': function(e) {
                console.log(e.type, e.dragType);
            },
            'dragend.cropper': function(e) {
                console.log(e.type, e.dragType);
            },
            'zoomin.cropper': function(e) {
                console.log(e.type);
            },
            'zoomout.cropper': function(e) {
                console.log(e.type);
            },
            'change.cropper': function(e) {
                console.log(e.type);
            }
        }).cropper(options);


        // Methods
        $(document.body).on('click', '[data-method]', function() {
            var data = $(this).data(),
                $target,
                result;

            if (!$image.data('cropper')) {
                return;
            }

            if (data.method) {
                data = $.extend({}, data); // Clone a new one

                if (typeof data.target !== 'undefined') {
                    $target = $(data.target);

                    if (typeof data.option === 'undefined') {
                        try {
                            data.option = JSON.parse($target.val());
                        } catch (e) {
                            console.log(e.message);
                        }
                    }
                }

                result = $image.cropper(data.method, data.option);

                if (data.method === 'getCroppedCanvas') {
                    $('#getCroppedCanvasModal').modal().find('.modal-body').html(result);
                }

                if ($.isPlainObject(result) && $target) {
                    try {
                        $target.val(JSON.stringify(result));
                    } catch (e) {
                        console.log(e.message);
                    }
                }

            }
        }).on('keydown', function(e) {

            if (!$image.data('cropper')) {
                return;
            }

            switch (e.which) {
                case 37:
                    e.preventDefault();
                    $image.cropper('move', -1, 0);
                    break;

                case 38:
                    e.preventDefault();
                    $image.cropper('move', 0, -1);
                    break;

                case 39:
                    e.preventDefault();
                    $image.cropper('move', 1, 0);
                    break;

                case 40:
                    e.preventDefault();
                    $image.cropper('move', 0, 1);
                    break;
            }

        });


        // Import image
        var $inputImage = $('#inputImage'),
            URL = window.URL || window.webkitURL,
            blobURL;

        if (URL) {
            $inputImage.change(function() {
                var files = this.files,
                    file;

                if (!$image.data('cropper')) {
                    return;
                }

                if (files && files.length) {
                    file = files[0];

                    if (/^image\/\w+$/.test(file.type)) {
                        blobURL = URL.createObjectURL(file);
                        $image.one('built.cropper', function() {
                            URL.revokeObjectURL(blobURL); // Revoke when load complete
                        }).cropper('reset').cropper('replace', blobURL);
                        $inputImage.val('');
                    } else {
                        alert('Please choose an image file.');
                    }
                }
            });
        } else {
            $inputImage.parent().remove();
        }


        // Options
        $('.docs-options :checkbox').on('change', function() {
            var $this = $(this);

            if (!$image.data('cropper')) {
                return;
            }

            options[$this.val()] = $this.prop('checked');
            $image.cropper('destroy').cropper(options);
        });


        // Tooltips
        $('[data-toggle="tooltip"]').tooltip();

    }

})();
// Select2
// -----------------------------------

(function() {
    'use strict';

    $(initSelect2);

    function initSelect2() {

        if (!$.fn.select2) return;

        // Select 2

        $('#select2-1').select2({
            theme: 'bootstrap4'
        });
        $('#select2-2').select2({
            theme: 'bootstrap4'
        });
        $('#select2-3').select2({
            theme: 'bootstrap4'
        });
        $('#select2-4').select2({
            placeholder: 'Select a state',
            allowClear: true,
            theme: 'bootstrap4'
        });

    }

})();
(function() {
    'use strict';

    if (typeof Dropzone === 'undefined') return;

    // Prevent Dropzone from auto discovering
    // This is useful when you want to create the
    // Dropzone programmatically later
    Dropzone.autoDiscover = false;

    $(initDropzone);

    function initDropzone() {

        // Dropzone settings
        var dropzoneOptions = {
            autoProcessQueue: false,
            uploadMultiple: true,
            parallelUploads: 100,
            maxFiles: 100,
            dictDefaultMessage: '<em class="fa fa-upload text-muted"></em><br>Drop files here to upload', // default messages before first drop
            paramName: 'file', // The name that will be used to transfer the file
            maxFilesize: 2, // MB
            addRemoveLinks: true,
            accept: function(file, done) {
                if (file.name === 'justinbieber.jpg') {
                    done('Naha, you dont. :)');
                } else {
                    done();
                }
            },
            init: function() {
                var dzHandler = this;

                this.element.querySelector('button[type=submit]').addEventListener('click', function(e) {
                    e.preventDefault();
                    e.stopPropagation();
                    dzHandler.processQueue();
                });
                this.on('addedfile', function(file) {
                    console.log('Added file: ' + file.name);
                });
                this.on('removedfile', function(file) {
                    console.log('Removed file: ' + file.name);
                });
                this.on('sendingmultiple', function() {

                });
                this.on('successmultiple', function( /*files, response*/ ) {

                });
                this.on('errormultiple', function( /*files, response*/ ) {

                });
            }

        };

        var dropzoneArea = new Dropzone('#dropzone-area', dropzoneOptions);

    }

})();
// Forms Demo
// -----------------------------------


(function() {
    'use strict';

    $(initWizard);

    function initWizard() {

        if (!$.fn.validate) return;

        // FORM EXAMPLE
        // -----------------------------------
        var form = $("#example-form");
        form.validate({
            errorPlacement: function errorPlacement(error, element) { element.before(error); },
            rules: {
                confirm: {
                    equalTo: "#password"
                }
            }
        });
        form.children("div").steps({
            headerTag: "h4",
            bodyTag: "fieldset",
            transitionEffect: "slideLeft",
            onStepChanging: function(event, currentIndex, newIndex) {
                form.validate().settings.ignore = ":disabled,:hidden";
                return form.valid();
            },
            onFinishing: function(event, currentIndex) {
                form.validate().settings.ignore = ":disabled";
                return form.valid();
            },
            onFinished: function(event, currentIndex) {
                alert("Submitted!");

                // Submit form
                $(this).submit();
            }
        });

        // VERTICAL
        // -----------------------------------

        $("#example-vertical").steps({
            headerTag: "h4",
            bodyTag: "section",
            transitionEffect: "slideLeft",
            stepsOrientation: "vertical"
        });

    }

})();
// Xeditable Demo
// -----------------------------------

(function() {
    'use strict';

    $(initXEditable);

    function initXEditable() {

        if (!$.fn.editable) return

        // Font Awesome support
        $.fn.editableform.buttons =
            '<button type="submit" class="btn btn-primary btn-sm editable-submit">' +
            '<i class="fa fa-fw fa-check"></i>' +
            '</button>' +
            '<button type="button" class="btn btn-default btn-sm editable-cancel">' +
            '<i class="fa fa-fw fa-times"></i>' +
            '</button>';

        //defaults
        //$.fn.editable.defaults.url = 'url/to/server';

        //enable / disable
        $('#enable').click(function() {
            $('#user .editable').editable('toggleDisabled');
        });

        //editables
        $('#username').editable({
            // url: 'url/to/server',
            type: 'text',
            pk: 1,
            name: 'username',
            title: 'Enter username',
            mode: 'inline'
        });

        $('#firstname').editable({
            validate: function(value) {
                if ($.trim(value) === '') return 'This field is required';
            },
            mode: 'inline'
        });

        $('#sex').editable({
            prepend: "not selected",
            source: [
                { value: 1, text: 'Male' },
                { value: 2, text: 'Female' }
            ],
            display: function(value, sourceData) {
                var colors = { "": "gray", 1: "green", 2: "blue" },
                    elem = $.grep(sourceData, function(o) { return o.value == value; });

                if (elem.length) {
                    $(this).text(elem[0].text).css("color", colors[value]);
                } else {
                    $(this).empty();
                }
            },
            mode: 'inline'
        });

        $('#status').editable({
            mode: 'inline'
        });

        $('#group').editable({
            showbuttons: false,
            mode: 'inline'
        });

        $('#dob').editable({
            mode: 'inline'
        });

        $('#event').editable({
            placement: 'right',
            combodate: {
                firstItem: 'name'
            },
            mode: 'inline'
        });

        $('#comments').editable({
            showbuttons: 'bottom',
            mode: 'inline'
        });

        $('#note').editable({
            mode: 'inline'
        });
        $('#pencil').click(function(e) {
            e.stopPropagation();
            e.preventDefault();
            $('#note').editable('toggle');
        });

        $('#user .editable').on('hidden', function(e, reason) {
            if (reason === 'save' || reason === 'nochange') {
                var $next = $(this).closest('tr').next().find('.editable');
                if ($('#autoopen').is(':checked')) {
                    setTimeout(function() {
                        $next.editable('show');
                    }, 300);
                } else {
                    $next.focus();
                }
            }
        });

        // TABLE
        // -----------------------------------

        $('#users a').editable({
            type: 'text',
            name: 'username',
            title: 'Enter username',
            mode: 'inline'
        });

    }

})();
// Full Calendar
// -----------------------------------


(function() {
    'use strict';

    // When dom ready, init calendar and events
    $(initFullCalendar);

    function initFullCalendar() {

        if (!$.fn.fullCalendar) return;

        // The element that will display the calendar
        var calendar = $('#calendar');

        var demoEvents = createDemoEvents();

        initExternalEvents(calendar);

        initCalendar(calendar, demoEvents);

    }


    // global shared var to know what we are dragging
    var draggingEvent = null;

    /**
     * ExternalEvent object
     * @param jQuery Object elements Set of element as jQuery objects
     */
    var ExternalEvent = function(elements) {

        if (!elements) return;

        elements.each(function() {
            var $this = $(this);
            // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
            // it doesn't need to have a start or end
            var calendarEventObject = {
                title: $.trim($this.text()) // use the element's text as the event title
            };

            // store the Event Object in the DOM element so we can get to it later
            $this.data('calendarEventObject', calendarEventObject);

            // make the event draggable using jQuery UI
            $this.draggable({
                zIndex: 1070,
                revert: true, // will cause the event to go back to its
                revertDuration: 0 //  original position after the drag
            });

        });
    };

    /**
     * Invoke full calendar plugin and attach behavior
     * @param  jQuery [calElement] The calendar dom element wrapped into jQuery
     * @param  EventObject [events] An object with the event list to load when the calendar displays
     */
    function initCalendar(calElement, events) {

        // check to remove elements from the list
        var removeAfterDrop = $('#remove-after-drop');

        calElement.fullCalendar({
            // isRTL: true,
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            buttonIcons: { // note the space at the beginning
                prev: ' fa fa-caret-left',
                next: ' fa fa-caret-right'
            },
            buttonText: {
                today: 'today',
                month: 'month',
                week: 'week',
                day: 'day'
            },
            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar
            drop: function(date, allDay) { // this function is called when something is dropped

                var $this = $(this),
                    // retrieve the dropped element's stored Event Object
                    originalEventObject = $this.data('calendarEventObject');

                // if something went wrong, abort
                if (!originalEventObject) return;

                // clone the object to avoid multiple events with reference to the same object
                var clonedEventObject = $.extend({}, originalEventObject);

                // assign the reported date
                clonedEventObject.start = date;
                clonedEventObject.allDay = allDay;
                clonedEventObject.backgroundColor = $this.css('background-color');
                clonedEventObject.borderColor = $this.css('border-color');

                // render the event on the calendar
                // the last `true` argument determines if the event "sticks"
                // (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                calElement.fullCalendar('renderEvent', clonedEventObject, true);

                // if necessary remove the element from the list
                if (removeAfterDrop.is(':checked')) {
                    $this.remove();
                }
            },
            eventDragStart: function(event, js, ui) {
                draggingEvent = event;
            },
            // This array is the events sources
            events: events
        });
    }

    /**
     * Inits the external events card
     * @param  jQuery [calElement] The calendar dom element wrapped into jQuery
     */
    function initExternalEvents(calElement) {
        // Card with the external events list
        var externalEvents = $('.external-events');

        // init the external events in the card
        new ExternalEvent(externalEvents.children('div'));

        // External event color is danger-red by default
        var currColor = '#f6504d';
        // Color selector button
        var eventAddBtn = $('.external-event-add-btn');
        // New external event name input
        var eventNameInput = $('.external-event-name');
        // Color switchers
        var eventColorSelector = $('.external-event-color-selector .circle');

        // Trash events Droparea
        $('.external-events-trash').droppable({
            accept: '.fc-event',
            activeClass: 'active',
            hoverClass: 'hovered',
            tolerance: 'touch',
            drop: function(event, ui) {

                // You can use this function to send an ajax request
                // to remove the event from the repository

                if (draggingEvent) {
                    var eid = draggingEvent.id || draggingEvent._id;
                    // Remove the event
                    calElement.fullCalendar('removeEvents', eid);
                    // Remove the dom element
                    ui.draggable.remove();
                    // clear
                    draggingEvent = null;
                }
            }
        });

        eventColorSelector.click(function(e) {
            e.preventDefault();
            var $this = $(this);

            // Save color
            currColor = $this.css('background-color');
            // De-select all and select the current one
            eventColorSelector.removeClass('selected');
            $this.addClass('selected');
        });

        eventAddBtn.click(function(e) {
            e.preventDefault();

            // Get event name from input
            var val = eventNameInput.val();
            // Dont allow empty values
            if ($.trim(val) === '') return;

            // Create new event element
            var newEvent = $('<div/>').css({
                    'background-color': currColor,
                    'border-color': currColor,
                    'color': '#fff'
                })
                .html(val);

            // Prepends to the external events list
            externalEvents.prepend(newEvent);
            // Initialize the new event element
            new ExternalEvent(newEvent);
            // Clear input
            eventNameInput.val('');
        });
    }

    /**
     * Creates an array of events to display in the first load of the calendar
     * Wrap into this function a request to a source to get via ajax the stored events
     * @return Array The array with the events
     */
    function createDemoEvents() {
        // Date for the calendar events (dummy data)
        var date = new Date();
        var d = date.getDate(),
            m = date.getMonth(),
            y = date.getFullYear();

        return [{
            title: 'All Day Event',
            start: new Date(y, m, 1),
            backgroundColor: '#f56954', //red
            borderColor: '#f56954' //red
        }, {
            title: 'Long Event',
            start: new Date(y, m, d - 5),
            end: new Date(y, m, d - 2),
            backgroundColor: '#f39c12', //yellow
            borderColor: '#f39c12' //yellow
        }, {
            title: 'Meeting',
            start: new Date(y, m, d, 10, 30),
            allDay: false,
            backgroundColor: '#0073b7', //Blue
            borderColor: '#0073b7' //Blue
        }, {
            title: 'Lunch',
            start: new Date(y, m, d, 12, 0),
            end: new Date(y, m, d, 14, 0),
            allDay: false,
            backgroundColor: '#00c0ef', //Info (aqua)
            borderColor: '#00c0ef' //Info (aqua)
        }, {
            title: 'Birthday Party',
            start: new Date(y, m, d + 1, 19, 0),
            end: new Date(y, m, d + 1, 22, 30),
            allDay: false,
            backgroundColor: '#00a65a', //Success (green)
            borderColor: '#00a65a' //Success (green)
        }, {
            title: 'Open Google',
            start: new Date(y, m, 28),
            end: new Date(y, m, 29),
            url: '//google.com/',
            backgroundColor: '#3c8dbc', //Primary (light-blue)
            borderColor: '#3c8dbc' //Primary (light-blue)
        }];
    }

})();
// JQCloud
// -----------------------------------


(function() {
    'use strict';

    $(initWordCloud);

    function initWordCloud() {

        if (!$.fn.jQCloud) return;

        //Create an array of word objects, each representing a word in the cloud
        var word_array = [
            { text: 'Lorem', weight: 13, /*link: 'http://themicon.co'*/ },
            { text: 'Ipsum', weight: 10.5 },
            { text: 'Dolor', weight: 9.4 },
            { text: 'Sit', weight: 8 },
            { text: 'Amet', weight: 6.2 },
            { text: 'Consectetur', weight: 5 },
            { text: 'Adipiscing', weight: 5 },
            { text: 'Sit', weight: 8 },
            { text: 'Amet', weight: 6.2 },
            { text: 'Consectetur', weight: 5 },
            { text: 'Adipiscing', weight: 5 }
        ];

        $("#jqcloud").jQCloud(word_array, {
            width: 240,
            height: 200,
            steps: 7
        });

    }

})();
// Search Results
// -----------------------------------


(function() {
    'use strict';

    $(initSearch);

    function initSearch() {

        if (!$.fn.slider) return;
        if (!$.fn.chosen) return;
        if (!$.fn.datepicker) return;

        // BOOTSTRAP SLIDER CTRL
        // -----------------------------------

        $('[data-ui-slider]').slider();

        // CHOSEN
        // -----------------------------------

        $('.chosen-select').chosen();

        // DATETIMEPICKER
        // -----------------------------------

        $('#datetimepicker').datepicker({
            orientation: 'bottom',
            icons: {
                time: 'fa fa-clock-o',
                date: 'fa fa-calendar',
                up: 'fa fa-chevron-up',
                down: 'fa fa-chevron-down',
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-crosshairs',
                clear: 'fa fa-trash'
            }
        });

    }

})();
/**=========================================================
 * Module: gmap.js
 * Init Google Map plugin
 =========================================================*/

(function() {
    'use strict';

    $(initGoogleMaps);

    // -------------------------
    // Map Style definition
    // -------------------------
    // Get more styles from http://snazzymaps.com/style/29/light-monochrome
    // - Just replace and assign to 'MapStyles' the new style array
    var MapStyles = [{ featureType: 'water', stylers: [{ visibility: 'on' }, { color: '#bdd1f9' }] }, { featureType: 'all', elementType: 'labels.text.fill', stylers: [{ color: '#334165' }] }, { featureType: 'landscape', stylers: [{ color: '#e9ebf1' }] }, { featureType: 'road.highway', elementType: 'geometry', stylers: [{ color: '#c5c6c6' }] }, { featureType: 'road.arterial', elementType: 'geometry', stylers: [{ color: '#fff' }] }, { featureType: 'road.local', elementType: 'geometry', stylers: [{ color: '#fff' }] }, { featureType: 'transit', elementType: 'geometry', stylers: [{ color: '#d8dbe0' }] }, { featureType: 'poi', elementType: 'geometry', stylers: [{ color: '#cfd5e0' }] }, { featureType: 'administrative', stylers: [{ visibility: 'on' }, { lightness: 33 }] }, { featureType: 'poi.park', elementType: 'labels', stylers: [{ visibility: 'on' }, { lightness: 20 }] }, { featureType: 'road', stylers: [{ color: '#d8dbe0', lightness: 20 }] }];


    function initGoogleMaps() {

        if (!$.fn.gMap) return;

        var mapSelector = '[data-gmap]';
        var gMapRefs = [];

        $(mapSelector).each(function() {

            var $this = $(this),
                addresses = $this.data('address') && $this.data('address').split(';'),
                titles = $this.data('title') && $this.data('title').split(';'),
                zoom = $this.data('zoom') || 14,
                maptype = $this.data('maptype') || 'ROADMAP', // or 'TERRAIN'
                markers = [];

            if (addresses) {
                for (var a in addresses) {
                    if (typeof addresses[a] == 'string') {
                        markers.push({
                            address: addresses[a],
                            html: (titles && titles[a]) || '',
                            popup: true /* Always popup */
                        });
                    }
                }

                var options = {
                    controls: {
                        panControl: true,
                        zoomControl: true,
                        mapTypeControl: true,
                        scaleControl: true,
                        streetViewControl: true,
                        overviewMapControl: true
                    },
                    scrollwheel: false,
                    maptype: maptype,
                    markers: markers,
                    zoom: zoom
                    // More options https://github.com/marioestrada/jQuery-gMap
                };

                var gMap = $this.gMap(options);

                var ref = gMap.data('gMap.reference');
                // save in the map references list
                gMapRefs.push(ref);

                // set the styles
                if ($this.data('styled') !== undefined) {

                    ref.setOptions({
                        styles: MapStyles
                    });

                }
            }

        }); //each

    }

})();
// jVectorMap
// -----------------------------------


(function() {
    'use strict';

    $(initVectorMap);

    function initVectorMap() {

        var element = $('[data-vector-map]');

        var seriesData = {
            'CA': 11100, // Canada
            'DE': 2510, // Germany
            'FR': 3710, // France
            'AU': 5710, // Australia
            'GB': 8310, // Great Britain
            'RU': 9310, // Russia
            'BR': 6610, // Brazil
            'IN': 7810, // India
            'CN': 4310, // China
            'US': 839, // USA
            'SA': 410 // Saudi Arabia
        };

        var markersData = [
            { latLng: [41.90, 12.45], name: 'Vatican City' },
            { latLng: [43.73, 7.41], name: 'Monaco' },
            { latLng: [-0.52, 166.93], name: 'Nauru' },
            { latLng: [-8.51, 179.21], name: 'Tuvalu' },
            { latLng: [7.11, 171.06], name: 'Marshall Islands' },
            { latLng: [17.3, -62.73], name: 'Saint Kitts and Nevis' },
            { latLng: [3.2, 73.22], name: 'Maldives' },
            { latLng: [35.88, 14.5], name: 'Malta' },
            { latLng: [41.0, -71.06], name: 'New England' },
            { latLng: [12.05, -61.75], name: 'Grenada' },
            { latLng: [13.16, -59.55], name: 'Barbados' },
            { latLng: [17.11, -61.85], name: 'Antigua and Barbuda' },
            { latLng: [-4.61, 55.45], name: 'Seychelles' },
            { latLng: [7.35, 134.46], name: 'Palau' },
            { latLng: [42.5, 1.51], name: 'Andorra' }
        ];

        new VectorMap(element, seriesData, markersData);

    }

})();
// JVECTOR MAP
// -----------------------------------

(function() {
    'use strict';

    // Allow Global access
    window.VectorMap = VectorMap

    var defaultColors = {
        markerColor: '#23b7e5', // the marker points
        bgColor: 'transparent', // the background
        scaleColors: ['#878c9a'], // the color of the region in the serie
        regionFill: '#bbbec6' // the base region color
    };

    function VectorMap(element, seriesData, markersData) {

        if (!element || !element.length) return;

        var attrs = element.data(),
            mapHeight = attrs.height || '300',
            options = {
                markerColor: attrs.markerColor || defaultColors.markerColor,
                bgColor: attrs.bgColor || defaultColors.bgColor,
                scale: attrs.scale || 1,
                scaleColors: attrs.scaleColors || defaultColors.scaleColors,
                regionFill: attrs.regionFill || defaultColors.regionFill,
                mapName: attrs.mapName || 'world_mill_en'
            };

        element.css('height', mapHeight);

        init(element, options, seriesData, markersData);

        function init($element, opts, series, markers) {

            $element.vectorMap({
                map: opts.mapName,
                backgroundColor: opts.bgColor,
                zoomMin: 1,
                zoomMax: 8,
                zoomOnScroll: false,
                regionStyle: {
                    initial: {
                        'fill': opts.regionFill,
                        'fill-opacity': 1,
                        'stroke': 'none',
                        'stroke-width': 1.5,
                        'stroke-opacity': 1
                    },
                    hover: {
                        'fill-opacity': 0.8
                    },
                    selected: {
                        fill: 'blue'
                    },
                    selectedHover: {}
                },
                focusOn: { x: 0.4, y: 0.6, scale: opts.scale },
                markerStyle: {
                    initial: {
                        fill: opts.markerColor,
                        stroke: opts.markerColor
                    }
                },
                onRegionLabelShow: function(e, el, code) {
                    if (series && series[code])
                        el.html(el.html() + ': ' + series[code] + ' visitors');
                },
                markers: markers,
                series: {
                    regions: [{
                        values: series,
                        scale: opts.scaleColors,
                        normalizeFunction: 'polynomial'
                    }]
                },
            });

        } // end init
    };

})();
/**
 * Used for user pages
 * Login and Register
 */
(function() {
    'use strict';

    $(initParsleyForPages)

    function initParsleyForPages() {

        // Parsley options setup for bootstrap validation classes
        var parsleyOptions = {
            errorClass: 'is-invalid',
            successClass: 'is-valid',
            classHandler: function(ParsleyField) {
                var el = ParsleyField.$element.parents('.form-group').find('input');
                if (!el.length) // support custom checkbox
                    el = ParsleyField.$element.parents('.c-checkbox').find('label');
                return el;
            },
            errorsContainer: function(ParsleyField) {
                return ParsleyField.$element.parents('.form-group');
            },
            errorsWrapper: '<div class="text-help">',
            errorTemplate: '<div></div>'
        };

        // Login form validation with Parsley
        var loginForm = $("#loginForm");
        if (loginForm.length)
            loginForm.parsley(parsleyOptions);

        // Register form validation with Parsley
        var registerForm = $("#registerForm");
        if (registerForm.length)
            registerForm.parsley(parsleyOptions);

    }

})();
// BOOTGRID
// -----------------------------------

(function() {
    'use strict';

    $(initBootgrid);

    function initBootgrid() {

        if (!$.fn.bootgrid) return;

        $('#bootgrid-basic').bootgrid({
            templates: {
                // templates for BS4
                actionButton: '<button class="btn btn-secondary" type="button" title="{{ctx.text}}">{{ctx.content}}</button>',
                actionDropDown: '<div class="{{css.dropDownMenu}}"><button class="btn btn-secondary dropdown-toggle dropdown-toggle-nocaret" type="button" data-toggle="dropdown"><span class="{{css.dropDownMenuText}}">{{ctx.content}}</span></button><ul class="{{css.dropDownMenuItems}}" role="menu"></ul></div>',
                actionDropDownItem: '<li class="dropdown-item"><a href="" data-action="{{ctx.action}}" class="dropdown-link {{css.dropDownItemButton}}">{{ctx.text}}</a></li>',
                actionDropDownCheckboxItem: '<li class="dropdown-item"><label class="dropdown-item p-0"><input name="{{ctx.name}}" type="checkbox" value="1" class="{{css.dropDownItemCheckbox}}" {{ctx.checked}} /> {{ctx.label}}</label></li>',
                paginationItem: '<li class="page-item {{ctx.css}}"><a href="" data-page="{{ctx.page}}" class="page-link {{css.paginationButton}}">{{ctx.text}}</a></li>',
            }
        });

        $('#bootgrid-selection').bootgrid({
            selection: true,
            multiSelect: true,
            rowSelect: true,
            keepSelection: true,
            templates: {
                select:
                    '<div class="checkbox c-checkbox">' +
                        '<label class="mb-0">' +
                            '<input type="{{ctx.type}}" class="{{css.selectBox}}" value="{{ctx.value}}" {{ctx.checked}}>' +
                            '<span class="fa fa-check"></span>' +
                        '</label>'+
                    '</div>',
                // templates for BS4
                actionButton: '<button class="btn btn-secondary" type="button" title="{{ctx.text}}">{{ctx.content}}</button>',
                actionDropDown: '<div class="{{css.dropDownMenu}}"><button class="btn btn-secondary dropdown-toggle dropdown-toggle-nocaret" type="button" data-toggle="dropdown"><span class="{{css.dropDownMenuText}}">{{ctx.content}}</span></button><ul class="{{css.dropDownMenuItems}}" role="menu"></ul></div>',
                actionDropDownItem: '<li class="dropdown-item"><a href="" data-action="{{ctx.action}}" class="dropdown-link {{css.dropDownItemButton}}">{{ctx.text}}</a></li>',
                actionDropDownCheckboxItem: '<li class="dropdown-item"><label class="dropdown-item p-0"><input name="{{ctx.name}}" type="checkbox" value="1" class="{{css.dropDownItemCheckbox}}" {{ctx.checked}} /> {{ctx.label}}</label></li>',
                paginationItem: '<li class="page-item {{ctx.css}}"><a href="" data-page="{{ctx.page}}" class="page-link {{css.paginationButton}}">{{ctx.text}}</a></li>',
            }
        });

        var grid = $('#bootgrid-command').bootgrid({
            formatters: {
                commands: function(column, row) {
                    return '<button type="button" class="btn btn-sm btn-info mr-2 command-edit" data-row-id="' + row.id + '"><em class="fa fa-edit fa-fw"></em></button>' +
                        '<button type="button" class="btn btn-sm btn-danger command-delete" data-row-id="' + row.id + '"><em class="fa fa-trash fa-fw"></em></button>';
                }
            },
            templates: {
                // templates for BS4
                actionButton: '<button class="btn btn-secondary" type="button" title="{{ctx.text}}">{{ctx.content}}</button>',
                actionDropDown: '<div class="{{css.dropDownMenu}}"><button class="btn btn-secondary dropdown-toggle dropdown-toggle-nocaret" type="button" data-toggle="dropdown"><span class="{{css.dropDownMenuText}}">{{ctx.content}}</span></button><ul class="{{css.dropDownMenuItems}}" role="menu"></ul></div>',
                actionDropDownItem: '<li class="dropdown-item"><a href="" data-action="{{ctx.action}}" class="dropdown-link {{css.dropDownItemButton}}">{{ctx.text}}</a></li>',
                actionDropDownCheckboxItem: '<li class="dropdown-item"><label class="dropdown-item p-0"><input name="{{ctx.name}}" type="checkbox" value="1" class="{{css.dropDownItemCheckbox}}" {{ctx.checked}} /> {{ctx.label}}</label></li>',
                paginationItem: '<li class="page-item {{ctx.css}}"><a href="" data-page="{{ctx.page}}" class="page-link {{css.paginationButton}}">{{ctx.text}}</a></li>',
            }
        }).on('loaded.rs.jquery.bootgrid', function() {
            /* Executes after data is loaded and rendered */
            grid.find('.command-edit').on('click', function() {
                console.log('You pressed edit on row: ' + $(this).data('row-id'));
            }).end().find('.command-delete').on('click', function() {
                console.log('You pressed delete on row: ' + $(this).data('row-id'));
            });
        });

    }

})();
// DATATABLES
// -----------------------------------

(function() {
    'use strict';

    $(initDatatables);

    function initDatatables() {

        if (!$.fn.DataTable) return;

        // Zero configuration

        $('#datatable1').DataTable({
            'paging': true, // Table pagination
            'ordering': true, // Column ordering
            'info': true, // Bottom left status text
            responsive: true,
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: '<em class="fas fa-search"></em>',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)',
                oPaginate: {
                    sNext: '<em class="fa fa-caret-right"></em>',
                    sPrevious: '<em class="fa fa-caret-left"></em>'
                }
            }
        });


        // Filter

        $('#datatable2').DataTable({
            'paging': true, // Table pagination
            'ordering': true, // Column ordering
            'info': true, // Bottom left status text
            responsive: true,
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: 'Search all columns:',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)',
                oPaginate: {
                    sNext: '<em class="fa fa-caret-right"></em>',
                    sPrevious: '<em class="fa fa-caret-left"></em>'
                }
            },
            // Datatable Buttons setup
            dom: 'Bfrtip',
            buttons: [
                { extend: 'copy', className: 'btn-info' },
                { extend: 'csv', className: 'btn-info' },
                { extend: 'excel', className: 'btn-info', title: 'XLS-File' },
                { extend: 'pdf', className: 'btn-info', title: $('title').text() },
                { extend: 'print', className: 'btn-info' }
            ]
        });

        $('#datatable3').DataTable({
            'paging': true, // Table pagination
            'ordering': true, // Column ordering
            'info': true, // Bottom left status text
            responsive: true,
            // Text translation options
            // Note the required keywords between underscores (e.g _MENU_)
            oLanguage: {
                sSearch: 'Search all columns:',
                sLengthMenu: '_MENU_ records per page',
                info: 'Showing page _PAGE_ of _PAGES_',
                zeroRecords: 'Nothing found - sorry',
                infoEmpty: 'No records available',
                infoFiltered: '(filtered from _MAX_ total records)',
                oPaginate: {
                    sNext: '<em class="fa fa-caret-right"></em>',
                    sPrevious: '<em class="fa fa-caret-left"></em>'
                }
            },
            // Datatable key setup
            keys: true
        });

    }

})();
// Custom Code
// -----------------------------------

(function() {
    'use strict';

    $(initCustom);

    function initCustom() {

        // custom code

    }

})();