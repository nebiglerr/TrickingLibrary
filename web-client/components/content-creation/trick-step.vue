<template>
  <v-card>
    <v-card-title>
      Create Tricks
      <v-spacer />
      <v-btn icon @click="close">
        <v-icon> mdi-close</v-icon>
      </v-btn>
    </v-card-title>
    <v-stepper v-model="step" class="rounded-0">
      <v-stepper-header class="elevation-0">
        <v-stepper-step :complete="step > 1" step="1">
          Trick Information
        </v-stepper-step>

        <v-divider />

        <v-stepper-step step="2">
          Review
        </v-stepper-step>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1" class="pt-0">
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
          <div class="d-flex justify-center">
            <v-btn @click="step++">
              Next
            </v-btn>
          </div>
        </v-stepper-content>

        <v-stepper-content step="2">
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
import { mapActions, mapGetters } from 'vuex'
import { close } from './_shared'

export default {
  name: 'TrickStep',
  mixins: [close],
  data: () => ({
    step: 1,
    form: {
      name: '',
      description: '',
      difficulty: '',
      prerequisites: [],
      progressions: [],
      categories: []
    },
    testData: [
      { text: 'Foo', value: 1 },
      { text: 'Bar', value: 2 },
      { text: 'FooBaz', value: 3 }
    ]

  }),

  computed: {
    ...mapGetters('tricks', ['categoriesItem', 'difficultiesItem', 'tricksItem'])
  },
  methods: {
    ...mapActions('tricks', ['createTricks']),

    async save () {
      await this.createTricks(this.form
      )
      this.close()
    }
  }
}
</script>

<style scoped>

</style>
