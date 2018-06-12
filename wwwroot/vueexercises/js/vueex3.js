

new Vue({
    el: '#trays',
    methods: {
        async getTray() {
            try {
                this.status = 'Loading... ';
                response = await axios.get("/GetTray");
                this.trays = response.data;
                this.status = '';
            } catch (error) {
                console.log(error.statusText);
            }
        },
    },
    mounted() { this.getTray(); },
    data: {
        trays: [],
        status: ''
    }
}); 

function formatDate(date) {
    let d;
    // see if date is coming from server
    date === undefined
        ? d = new Date()
        : d = new Date(Date.parse(date)); // date from server
    let _day = d.getDate();
    let _month = d.getMonth() + 1;
    let _year = d.getFullYear();
    let _hour = d.getHours();
    let _min = d.getMinutes();
    if (_min < 10) { _min = "0" + _min; }
    return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
}
