/* jshint esversion: 6, strict:global */
/* globals console, Vue */
"use strict";

Date.prototype.getGermenStr = function () {
    return this.getDate() + "." +
        ("00" + (this.getMonth() + 1)).slice(-2) + "." +
        this.getFullYear();
}
const vm = new Vue({
    el: '#weatherApp',
    data: {
        pos: { lat: 48, lng: 16.4, height: 200 },
        forecast: {},
        error: ""
    },
    computed: {
        maxTemps: function () {
            try {
                const forecasts = this.forecast
                    .forecasts
                    .map(f => { f.time = new Date(f.time); return f })
                    .filter(f => f.time.getUTCHours() == 18);
                return forecasts
            }
            catch {
                return {};
            }

        }
    },
    methods: {
        getForecast: async function (event) {
            const response = await fetch(`forecast?lat=${this.pos.lat}&lng=${this.pos.lng}&height=${this.pos.height}`);
            if (response.ok) {
                this.forecast = await response.json();
            }
            else {
                this.error = "Error fetching from Webservice.";
            }
        }
    }
});

