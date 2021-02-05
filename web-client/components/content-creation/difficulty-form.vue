<template>
  <v-card>
    <v-card-title>
      Create Difficulty
      <v-spacer />
      <v-btn icon @click="close">
        <v-icon> mdi-close</v-icon>
      </v-btn>
    </v-card-title>
    <v-card-text>
      <v-text-field v-model="form.name" label="Name" />
      <v-text-field v-model="form.description" label="Description" />
    </v-card-text>
    <v-card-actions class="d-flex justify-center">
      <v-btn @click="save">
        Save
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>

import { close } from '~/components/content-creation/_shared'

export default {
  name: 'DifficultyForm',
  mixins: [close],
  data: () => ({
    form: {
      name: '',
      description: ''

    }

  }),
  methods: {
    async save () {
      await this.$axios.post('api/difficulties', this.form)
      this.close()
    }
  }
}
</script>

<style scoped>

</style>
