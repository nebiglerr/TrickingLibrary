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
            <v-text-field v-model="form.name" label="Name" />
            <v-text-field v-model="form.description" label="Description" />
            <v-select v-model="form.difficulty" :items="difficultiesItem" label="Difficulty" />
            <v-select v-model="form.prerequisites" :items="tricksItem" label="Prerequisites" multiple chips />
            <v-select v-model="form.progressions" :items="tricksItem" label="Progressions" multiple small-chips />
            <v-select
              v-model="form.categories"
              :items="categoriesItem"
              label="Category"
              multiple
              small-chips
              deletable-chips
            />
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
import { mapActions, mapGetters, mapMutations } from 'vuex'

const initState = () => ({
  step: 1,
  form: {
    name: '',
    description: '',
    difficulty: '',
    prerequisites: [],
    progressions: [],
    categories: []
  }

})
export default {
  name: 'TrickStep',

  data: initState,

  computed: {
    ...mapGetters('tricks', ['categoriesItem', 'difficultiesItem', 'tricksItem'])
  },
  methods: {
    ...mapMutations('video-upload', ['resetVideos']),
    ...mapActions('tricks', ['createTricks']),

    async save () {
      await this.createTricks(this.form
      )
      this.resetVideos()
      Object.assign(this.$data, initState())
    }
  }
}
</script>

<style scoped>

</style>
