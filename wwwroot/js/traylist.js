// register modal component
Vue.component("modal", {
    template: "#modal-template",
    props: {
        tray: {},
        details: []
    },
})
// main Vue component
new Vue({
    el: "#trays",
    methods: {
        async getTrays() {
            try {
                this.status = "Loading Trays..."
                response = await axios.get("/GetTrays");
                this.trays = response.data;
                this.status = ""
            } catch (error) {
                console.log(error.statusText);
            }
        },
        async loadAndShowModal() {
            try {
                this.modalStatus = "Loading Details..";
                this.showModal = true;
                response = await axios.get(`/GetTrayDetails/${this.trayForModal.id}`);
                this.detailsForModal = response.data;
                this.modalStatus = "";
            } catch (error) {
                console.log(error.statusText);
            }
        },
    },
    mounted() { this.getTrays() },
    data: {
        trays: [],
        showModal: false,
        trayForModal: {},
        detailsForModal: [],
        status: "",
        modalStatus: ""
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