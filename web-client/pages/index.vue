<template>
  <div>
    <div>
      <v-btn @click="api('test')">
        Api
      </v-btn>
      <v-btn @click="api('mod')">
        Mod
      </v-btn>
    </div>
    <div v-for="s in sections" :key="s.id">
      <div class="d-flex flex-column align-center">
        <p class="text-h5">
          {{ s.title }}
        </p>
        <div>
          <v-btn v-for="item in s.collections" :key="item.id" class="mx-1" :to="s.routeFactory(item.id)">
            {{ item.name }}
          </v-btn>
        </div>
      </div>
      <v-divider class="my-5" />
    </div>
  </div>
</template>

<script>

import * as https from 'https'
import { mapState } from 'vuex'

import axios from 'axios'
axios.defaults.httpsAgent = new https.Agent({
  rejectUnauthorized: false
})
export default {

  computed: {
    ...mapState('tricks', ['tricks', 'categories', 'difficulties']),

    sections () {
      return [
        { collections: this.tricks, title: 'Tricks', routeFactory: id => '/trick/' + id },
        { collections: this.categories, title: 'Categories', routeFactory: id => '/category/' + id },
        { collections: this.difficulties, title: 'Difficulties', routeFactory: id => '/difficulty/' + id }
      ]
    }
  },
  methods: {
    api (x) {
      return this.$axios.$get('/api/tricks/' + x).then((msg) => { console.log(msg) })
    }
  }

}
</script>
