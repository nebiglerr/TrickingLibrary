const initState = () => ({
  user: null,
  loading: true
})

export const state = initState
const ROLES = {
  MODERATOR: 'Mod'
}
export const getters = {
  authenticated: state => !state.loading && state.user !== null,
  moderator: (state, getters) => getters.authenticated && state.user.profile.role === ROLES.MODERATOR
}
export const mutations = {
  saveUser (state, { user }) {
    state.user = user
  },
  finish (state) {
    state.loading = false
  }
}

export const actions = {
  initialize ({ commit }) {
    this.$auth.getUser()
      .then((user) => {
        if (user) {
          commit('saveUser', { user })
          this.$axios.setToken('Bearer' + user.access_token)
        }
      }).finally(() => commit('finish'))
  }
}
