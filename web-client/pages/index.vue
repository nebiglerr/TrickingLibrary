<template>
  <div>
    <v-row justify="center" align="center">
      <v-col cols="12" sm="8" md="6">
        <div v-if="tricks">
          <p v-for="t in tricks" :key="t.id">
            {{ t.name }}
          </p>
        </div>

        <v-card>
          <v-stepper v-model="step">
            <v-stepper-header>
              <v-stepper-step :complete="step > 1" step="1">
                Upload Video
              </v-stepper-step>

              <v-divider />

              <v-stepper-step :complete="step > 2" step="2">
                Trick Information
              </v-stepper-step>

              <v-divider />

              <v-stepper-step step="3">
                Confirmation
              </v-stepper-step>
            </v-stepper-header>

            <v-stepper-items>
              <v-stepper-content step="1">
                <v-file-input accept="video/*" @change="handleFile($event)" />
              </v-stepper-content>

              <v-stepper-content step="2">
                <div>
                  <v-text-field v-model="trickName" label="Tricking Name" />
                  <v-btn @click="saveTrick">
                    Save Tricks
                  </v-btn>
                  <div />
                </div>
              </v-stepper-content>

              <v-stepper-content step="3">
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
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapState, mapActions, mapMutations } from 'vuex'

export default {

  data: () => ({
    trickName: '',
    step: 1

  }),
  computed: {
    ...mapState('tricks', ['tricks']),
    ...mapState('video', ['uploadPromise'])
  },

  methods: {
    ...mapActions('tricks', ['createTricks']),
    ...mapActions('video', ['startVideoUpload']),
    ...mapMutations('video', {
      resetVideos: 'resetVideos'
    }),

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
    async saveTrick () {
      'use strict'
      if (!this.uploadPromise) {
        //     console.log('uploadPromise İS NULL')
        return
      }
      const video = this.uploadPromise
      await this.createTricks({ tricks: { name: this.trickName, video } })
      this.trickName = ''
      this.step++
      this.resetVideos()
    }
  }

}
</script>
