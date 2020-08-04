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