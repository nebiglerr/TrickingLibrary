<template>
  <div>
    <div>
      <v-btn @click="login">
        Login
      </v-btn>
    </div>
    <div v-for="s in sections" :key="s.id">
      <div class="d-flex flex-column align-center">
        <p class="text-h5">
          {{ s.title }}
        </p>
        <div>
          <v-btn v-for="item in s.collections" :key="item.id" class="mx-1" :to="s.routeFactory(item.id)">
            {{ item.name }}
          </v-btn>
        </div>
      </div>
      <v-divider class="my-5" />
    </div>
  </div>
</template>

<script>

import { mapState } from 'vuex'
import { UserManager, WebStorageStateStore } from 'oidc-client'

export default {
  data: () => ({
    usrMgr: null
  }),
  computed: {
    ...mapState('tricks', ['tricks', 'categories', 'difficulties']),

    sections () {
      return [
        { collections: this.tricks, title: 'Tricks', routeFactory: id => '/trick/' + id },
        { collections: this.categories, title: 'Categories', routeFactory: id => '/category/' + id },
        { collections: this.difficulties, title: 'Difficulties', routeFactory: id => '/difficulty/' + id }
      ]
    }
  },
  created () {
    if (!process.server) {
      this.usrMgr = new UserManager({
        authority: 'http://localhost:5000',
        client_id: 'web-client',
        redirect_uri: 'http://localhost:3000',
        response_type: 'code',
        scope: 'openid profile',
        // loadUserInfo:true,
        post_logout_redirect_uri: 'http://localhost:3000',
        // silent_redirect_uri: 'http://localhost:3000/',
        userStore: new WebStorageStateStore({ store: localStorage })
        // store: window.localStorage
      })
    }

    const { code, scope, state } = this.$route.query
    if (code && scope && state) {
      this.usrMgr.signinRedirectCallback().then((user) => {
        console.log(user)
        this.$router.push('/')
      })
    }
  },
  methods: {
    login () {
      return this.usrMgr.signinRedirect(UserManager)
    }
  }

}
</script>
