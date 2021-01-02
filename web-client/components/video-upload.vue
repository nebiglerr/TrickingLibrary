<template>
  <v-dialog :value="active" persistent width="500">
    <v-card>
      <v-stepper v-model="step">
        <v-stepper-header>
          <v-stepper-step :complete="step > 1" step="1">
            Select Type
          </v-stepper-step>

          <v-divider />

          <v-stepper-step :complete="step > 2" step="2">
            Upload Video
          </v-stepper-step>
          <v-divider />

          <v-stepper-step :complete="step > 3" step="3">
            Trick Information
          </v-stepper-step>
          <v-divider />

          <v-stepper-step step="4">
            Review
          </v-stepper-step>
        </v-stepper-header>

        <v-stepper-items>
          <v-stepper-content step="1">
            <div class="d-flex flex-column align-center">
              <v-btn class="my-2" @click="setType(uploadType.TRICK)">
                Tricks
              </v-btn>
              <v-btn class="my-2" @click="setType(uploadType.SUBMISSION)">
                Submission
              </v-btn>
            </div>
          </v-stepper-content>
          <v-stepper-content step="2">
            <v-file-input accept="video/*" @change="handleFile($event)" />
          </v-stepper-content>

          <v-stepper-content step="3">
            <div>
              <v-text-field v-model="trickName" label="Tricking Name" />
              <v-btn @click="saveTrick">
                Save Tricks
              </v-btn>
              <div />
            </div>
          </v-stepper-content>

          <v-stepper-content step="4">
            <div>
              Success
            </div>

            <v-btn color="primary" @click="step = 1">
              Continue
            </v-btn>

            <v-btn text>
              Cancel
            </v-btn>
          </v-stepper-content>
        </v-stepper-items>
      </v-stepper>
    </v-card>
    <div class="d-flex justify-center my-4">
      <v-btn @click="toggleActivity">
        Closed
      </v-btn>
    </div>
  </v-dialog>
</template>

<script>
import { mapState, mapActions, mapMutations } from 'vuex'
import { UPLOAD_TYPE } from '~/data/enum'

export default {
  name: 'VideoUpload',

  data: () => ({
    trickName: ''

  }),
  computed: {
    ...mapState('video-upload', ['uploadPromise', 'active', 'step']),
    uploadType () {
      return {
        ...UPLOAD_TYPE
      }
    }
  },
  methods: {
    ...mapMutations('video-upload', ['resetVideos', 'toggleActivity', 'setType']),
    ...mapActions('video-upload', ['startVideoUpload', 'createTricks']),

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
    },
    async saveTrick () {
      if (!this.uploadPromise) {
        return
      }
      const video = await this.uploadPromise
      console.log(video)
      await this.createTricks({ tricks: { name: this.trickName, video } })
      this.trickName = ''

      this.resetVideos()
    }
  }
}
</script>

<style scoped>

</style>
