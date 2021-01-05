<template>
  <v-card>
    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">
          Trick Information
        </v-stepper-step>

        <v-divider />

        <v-stepper-step step="2">
          Review
        </v-stepper-step>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1">
          <div>
            <v-text-field v-model="form.name" label="Tricking Name" />
            <v-btn @click="step++">
              Next
            </v-btn>
            <div />
          </div>
        </v-stepper-content>

        <v-stepper-content step="2">
          <div>
            <v-btn @click="save">
              Save
            </v-btn>
          </div>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </v-card>
</template>

<script>
import { mapActions, mapMutations, mapState } from 'vuex'

const initState = () => ({
  step: 1,
  form: {
    name: ''
  }

})
export default {
  name: 'TrickStep',

  data: initState,
  computed: mapState('video-upload', ['active']),
  watch: {
    active (newValue) {
      if (!newValue) {
        Object.assign(this.$data, initState())
      }
    }
  },
  methods: {
    ...mapMutations('video-upload', ['resetVideos']),
    ...mapActions('tricks', ['createTricks']),

    async save () {
      await this.createTricks({
        form: this.form
      })
      this.resetVideos()
      Object.assign(this.$data, initState())
    }
  }
}
</script>

<style scoped>

</style>
