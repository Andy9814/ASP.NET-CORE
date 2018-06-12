// the following two blocks of code are components
// we'll use them to make custom html elements
Vue.component("hello", {
    template: "<label>Input Your Name:<h1></h1><input/></label>"
});
Vue.component("bar", {
    template: "<div id='bar'></div>"
});
//next we'll select an element
//set up data
//and define a method
new Vue({
    el: "#vue",
    data: {
        text: "This is a Vue app.",
        html: "<p id='green'>You can use Vue to do<br/>all kinds of cool stuff!</p>",
        html1: "<p>Here we'll use Vue to make an app<br/>that says Hello! to you.</p>"
    },
    methods: {
        sayHi() {
            let name = document.querySelector("#vue input"),
                button = document.querySelector("#vue button"),
                prompt = "What's your name?";
            let style = name.style;
            if (!name.value || name.value === prompt) {
                name.value = prompt;
                name.focus();
            } else {
                alert("Hello " + name.value + "!");
                name.value = null;
                button.blur();
            }
        }
    }
});