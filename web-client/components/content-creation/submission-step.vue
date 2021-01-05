<template>
  <v-card>
    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">
          Upload Video
        </v-stepper-step>

        <v-divider />

        <v-stepper-step :complete="step > 2" step="2">
          Select Tricks
        </v-stepper-step>
        <v-divider />

        <v-stepper-step :complete="step > 3" step="3">
          Submission
        </v-stepper-step>
        <v-divider />

        <v-stepper-step step="4">
          Review
        </v-stepper-step>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1">
          <v-file-input accept="video/*" @change="handleFile($event)" />
        </v-stepper-content>
        <v-stepper-content step="2">
          <div>
            <v-select v-model="form.trickId" :items="tricksItem" label="Select Trick" />

            <v-btn @click="step++">
              Next
            </v-btn>
            <div />
          </div>
        </v-stepper-content>

        <v-stepper-content step="3">
          <div>
            <v-text-field v-model="form.description" label="Description" />
            <v-btn @click="step++">
              Next
            </v-btn>
            <div />
          </div>
        </v-stepper-content>

        <v-stepper-content step="4">
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
import { mapState, mapGetters, mapActions, mapMutations } from 'vuex'

const initState = () => ({
  step: 1,
  form: {
    trickId: '',
    video: '',
    description: ''
  }

})
export default {
  name: 'SubmissionStep',

  data: initState,
  computed: {
    ...mapGetters('tricks', ['tricksItem']),
    ...mapState('video-upload', ['active'])
  },
  watch: {
    active (newValue) {
      if (!newValue) {
        Object.assign(this.$data, initState())
      }
    }
  },
  methods: {
    ...mapMutations('video-upload', ['hide']),
    ...mapActions('video-upload', ['startVideoUpload', 'createSubmission']),

    async handleFile (files) {
      'use strict'

      if (files == null) {
        return
      }

      const formData = new FormData()
      console.log(files)

      formData.append('video', files)
      // console.log(formData)
      await this.startVideoUpload({ formData })
      this.step++
    },
    save () {
      this.createSubmission({
        form: this.form
      })

      this.hide()
    }
  }
}
</script>

<style scoped>

</style>
