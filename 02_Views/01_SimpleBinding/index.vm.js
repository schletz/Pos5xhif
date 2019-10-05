/* jshint esversion: 6, strict:global */
/* globals console, Vue */
"use strict";
const vm = new Vue({
  el: '#myFirstApp',
  data: {
    names: [],
    newName: "",
  },
  computed: {
    // this zeigt auf das Viewmodel. Wird beim Aufruf der Methode von Vue.js so gesetzt.
    hasName: function () {
      return this.newName != "";
    }
  },
  methods: {
    addName: function (event) {
      if (this.newName != "") { this.names.push(this.newName); }
      this.newName = "";
    }
  }
});

