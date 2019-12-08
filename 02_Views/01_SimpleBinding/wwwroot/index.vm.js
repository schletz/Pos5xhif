/* jshint esversion: 6, strict:global */
/* globals console, Vue */
"use strict";

const vm = new Vue({
  el: '#myFirstApp',
  data: {
    title: "Beispiel 1: Binding mit Vue.js",
    names: [],
    newName: "",
  },
  computed: {
    // Arrow Functions funktionieren nicht, da this nicht auf data im Viewmodel zeigen würde.
    hasName: function () {
      // this zeigt auf data im Viewmodel. Wird beim Aufruf der Methode von Vue.js so gesetzt.
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

