<template>
  <v-card>
    <v-card-title>
      Create Submission
      <v-spacer />
      <v-btn icon @click="close">
        <v-icon> mdi-close</v-icon>
      </v-btn>
    </v-card-title>
    <v-stepper v-model="step" class="rounded-0">
      <v-stepper-header class="elevation-0">
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
        <v-stepper-content step="1" class="pt-0">
          <v-file-input accept="video/*" @change="handleFile($event)" />
        </v-stepper-content>
        <v-stepper-content step="2" class="pt-0">
          <div>
            <v-select v-model="form.trickId" :items="tricksItem" label="Select Trick" />

            <div class="d-flex justify-center">
              <v-btn @click="step++">
                Next
              </v-btn>
            </div>
          </div>
        </v-stepper-content>

        <v-stepper-content step="3" class="pt-0">
          <div>
            <v-text-field v-model="form.description" label="Description" />
            <div class="d-flex justify-center">
              <v-btn @click="step++">
                Next
              </v-btn>
            </div>
          </div>
        </v-stepper-content>

        <v-stepper-content step="4">
          <div class="d-flex justify-center">
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
import { mapGetters, mapActions, mapMutations } from 'vuex'
import { close } from '~/components/content-creation/_shared'

export default {
  name: 'SubmissionStep',
  mixins: [close],
  data: () => ({
    step: 1,
    form: {
      trickId: '',
      video: '',
      description: ''
    }

  }),
  computed: {
    ...mapGetters('tricks', ['tricksItem'])
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
