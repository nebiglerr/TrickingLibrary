<template>
  <div>
    <v-row justify="center" align="center">
      <v-col cols="12" sm="8" md="6">
        <div v-if="tricks">
          <div v-for="t in tricks" :key="t.id">
            <v-btn :to="'tricks/'+t.id">
              {{ t.name }}
            </v-btn>
          </div>
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  async fetch () {
    await this.$store.dispatch('tricks/fetchTricks', null, { root: true })
  },
  computed: {
    ...mapState('tricks', ['tricks'])
  }

}
</script>
