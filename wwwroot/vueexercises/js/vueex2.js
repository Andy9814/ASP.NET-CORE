new Vue({
    el: '#categories',
    methods: {
        async getCategories() {
            try {
                this.status = 'Loading... ';
                response = await axios.get("/GetCategories");
                this.categories = response.data;
                this.status = '';
            } catch (error) {
                console.log(error.statusText);
            }
        },
    },
    mounted() { this.getCategories(); },
    data: {
        categories: [],
        status: ''
    }
});
