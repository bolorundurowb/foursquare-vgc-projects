<template>
  <el-container class="AttendeeRegistration" v-loading="isLoading">
    <el-main class="AttendeeRegistration__main-content">
      <el-row type="flex" justify="center" class="AttendeeRegistration__row-container">
        <el-col :xs="24" :sm="12" :md="8" :lg="8" :xl="5">
          <el-card shadow="never">
            <img
              src="@/assets/logo.png"
              alt="logo"
              class="AttendeeRegistration__logo"
            >
            <h2 class="AttendeeRegistration__title">
              Foursquare Gospel Church Venue Mgt
            </h2>

            <div v-if="showNewPersonForm">
              <new-person-form
                :loading="newPersonLoading"
                @submit="handleNewPersonSubmit"
              />
            </div>

            <div v-if="showCheckPersonForm">
              <person-check-form
                :loading="this.checkPersonLoading"
                @submit="handlePersonCheckSubmit"
              />
            </div>

            <div v-if="showSeatSelectionForm">
              <seat-selection-form
                :selected-person="selectedPerson"
                :loading="seatSelectionLoading"
                @submit="handleSeatSelectionFormSubmit"
              />
            </div>

            <div v-if="showAssignedSeat">
              <el-descriptions
                title="Assigned Seat Information"
                direction="vertical"
                border
                :column="2"
              >
                <el-descriptions-item label="Venue Name">
                  {{assignedSeat.venueName}}
                </el-descriptions-item>

                <el-descriptions-item label="Seat Category">
                  <el-tag size="small">{{assignedSeat.category}}</el-tag>
                </el-descriptions-item>

                <el-descriptions-item label="Seat Number" :span="assignedSeat.associatedNumber ? 1 : 2">
                  <el-tag size="default" style="font-size: 1.3rem">{{assignedSeat.number}}</el-tag>
                </el-descriptions-item>

                <el-descriptions-item label="Associated Seat Number" v-if="assignedSeat.associatedNumber">
                  <el-tag size="default" style="font-size: 1.3rem">{{assignedSeat.associatedNumber}}</el-tag>
                </el-descriptions-item>
              </el-descriptions>

              <el-button @click="handleClose" type="primary" class="AttendeeRegistration__close-btn">
                Close
              </el-button>
            </div>

            <div v-if="showNewPersonForm || showCheckPersonForm || showSeatSelectionForm">
              <el-button
                icon="el-icon-back"
                type="text"
                @click="goBackToList"
              >
                Go back
              </el-button>
            </div>

            <div
              class="AttendeeRegistration__person-list"
              v-if="!(showNewPersonForm || showCheckPersonForm || showSeatSelectionForm || showAssignedSeat)"
            >
              <p>Choose a user registered on the device</p>

              <div class="AttendeeRegistration__person-container">
                <div
                  class="AttendeeRegistration__person-item AttendeeRegistration__person"
                  v-for="(person, personIndex) in cachedUsers"
                  :key="person.id"
                  @click="handlePersonClick(personIndex)"
                >
                  <el-avatar icon="el-icon-user-solid" />
                  <div class="AttendeeRegistration__person-details">
                    <p class="AttendeeRegistration__person-name">{{ person.fullName }}</p>
                    <p class="AttendeeRegistration__person-phone">{{ person.phone }}</p>
                  </div>
                </div>
              </div>

              <div class="AttendeeRegistration__person-item">
                <el-button type="text" @click="showCheckPersonForm = true">
                  Registered User? Enter your phone number here.
                </el-button>
              </div>

              <div class="AttendeeRegistration__person-item">
                <el-button type="text" @click="showNewPersonForm = true">
                  Not Registered? Register here.
                </el-button>
              </div>
            </div>
          </el-card>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script>
import api from '@/utils/api';
import { AlertMixin } from '@/mixins';
import NewPersonForm from '@/components/NewPersonForm.vue';
import PersonCheckForm from '@/components/PersonCheckForm.vue';
import SeatSelectionForm from '@/components/SeatSelectionForm.vue';

export default {
  name: 'AttendeeRegistration',
  mixins: [AlertMixin],
  components: {
    NewPersonForm,
    PersonCheckForm,
    SeatSelectionForm
  },
  props: {
    eventId: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      isLoading: true,
      cachedUsers: [],
      assignedSeat: null,
      selectedPerson: null,
      showAssignedSeat: false,

      newPersonLoading: false,
      showNewPersonForm: false,

      checkPersonLoading: false,
      showCheckPersonForm: false,

      seatSelectionLoading: false,
      showSeatSelectionForm: false
    }
  },
  methods: {
    async checkPerson({ phoneNumber }) {
      this.checkPersonLoading = true;

      try {
        const { data } = await api.get(`/v1/persons/check?phoneNumber=${phoneNumber}`);

         this.selectedPerson = data;
        this.cachePerson(data);
        this.showCheckPersonForm = false;
        this.showSeatSelectionForm = true;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.checkPersonLoading = false;
      }
    },
    async registerNewPerson(body) {
      this.newPersonLoading = true;

      try {
        const { data } = await api.post('/v1/persons', body);

        this.selectedPerson = data;
        this.cachePerson(data);
        this.showNewPersonForm = false;
        this.showSeatSelectionForm = true;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.newPersonLoading = false;
      }
    },
    async attenndeeCheckin(body) {
      this.seatSelectionLoading = true;

      try {
        const { data } = await api.post(`/v1/events/${this.eventId}/checkin`, body);

        this.selectedPerson = null;
        this.showSeatSelectionForm = false;
        this.assignedSeat = data;
        this.showAssignedSeat = true;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.seatSelectionLoading = false;
      }
    },
    handleShowRegisteredUser() {},
    handleNewPersonSubmit(body) {
      this.registerNewPerson(body);
    },
    handlePersonCheckSubmit(body) {
      this.checkPerson(body);
    },
    handleSeatSelectionFormSubmit(body) {
      const payload = {
        ...body,
        personId: this.selectedPerson.id
      };

      this.attenndeeCheckin(payload);
    },
    cachePerson(person) {
      const found = this.cachedUsers.find(user => user.id === person.id);

      if (!found) {
        this.cachedUsers.push(person);
      }

      localStorage.setItem('cachedusers', JSON.stringify(this.cachedUsers));
    },
    goBackToList() {
      this.showNewPersonForm = false;
      this.showCheckPersonForm = false;
      this.showSeatSelectionForm = false;

      this.selectedPerson = null;
    },
    handleClose() {
      window.close();
    },
    handlePersonClick(index) {
      this.selectedPerson = this.cachedUsers[index];
      this.showSeatSelectionForm = true;
    }
  },
  mounted() {
    const cachedUsers = localStorage.getItem('cachedusers');

    if (cachedUsers) {
      try {
        this.cachedUsers = JSON.parse(cachedUsers);
      } catch (error) {
        console.log(error);
      }
    }

    this.isLoading = false;
  },
  beforeDestroy() {
    this.selectedPerson = null;
    this.assignedSeat = null;
  }
}
</script>

<style lang="scss">
.AttendeeRegistration {
  height: 100vh;
  background-color: #F7F7F8;

  &__row-container {
    margin-top: 100px;
  }

  &__logo {
    height: 70px;
    width: 70px;
  }

  &__title {
    font-weight: 500;
    font-size: 20px;
  }

  &__person-item {
    padding: 5px;
  }

  &__person {
    cursor: pointer;
    display: flex;
    padding: 10px;

    p {
      margin: 0;
    }

    &:hover {
      background-color: #F7F7F8;
    }

    &:last-of-type {
      margin-bottom: 20px;
    }
  }

  &__person-details {
    margin-left: 10px;
  }

  &__person-name {
    font-size: 18px;
  }

  &__person-phone {
    font-size: 16px;
    color: #a9acb0;
  }

  &__close-btn {
    margin-top: 15px;
  }
}
</style>
